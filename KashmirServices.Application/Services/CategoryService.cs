using AutoMapper;
using KashmirServices.Application.Abstractions.Identity;
using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;

namespace KashmirServices.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository repository;
    private readonly IMapper mapper;
    private readonly IContextService contextService;
    private readonly IFileService fileService;
    private readonly IServiceTypeRepository serviceTypeRepository;
    private readonly IBaseRepository<AppFile> fileRepository;

    public CategoryService(ICategoryRepository repository,
        IMapper mapper,
        IContextService contextService,
        IFileService fileService,
        IServiceTypeRepository productSolutionRepository,
        IBaseRepository<AppFile> fileRepository)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.contextService = contextService;
        this.fileService = fileService;
        this.serviceTypeRepository = productSolutionRepository;
        this.fileRepository = fileRepository;

    }

    public async Task<APIResponse<CategoryResponse>> Add(CategoryRequest model)
    {
        var categoryExisted = await repository.FirstOrDefaultAsync(x => x.Name == model.Name && x.IsDeleted == false) != null;

        if (categoryExisted)
            return APIResponse<CategoryResponse>.ErrorResponse("Category already existed", APIStatusCodes.Conflict);

        var category = mapper.Map<Category>(model);
        category.Id = Guid.NewGuid();
        category.CreatedBy = contextService.GetUserId();

        var returnResult = await repository.AddCategory(category);
        
        if(returnResult > 0)
        {
            string filePath = "";
            if(model.File !=null)
            {
                filePath = await fileService.UploadFileAsync(model.File);
                AppFile file = new()
                {
                    Id = Guid.NewGuid(),
                    Module = Domain.Enums.Module.Category,
                    CreatedBy = contextService.GetUserId(),
                    CreatedOn = DateTimeOffset.Now,
                    EntityId = category.Id,
                    FilePath = filePath
                };
                var returnCode=  await repository.AddAppFile(file);
            }
            if (returnResult > 0)
            {
                var response = mapper.Map<CategoryResponse>(category);
                response.FilePath = filePath;
                return APIResponse<CategoryResponse>.SuccessResponse(response,APIStatusCodes.OK,"Category added successfully");
            }
        }
        return APIResponse<CategoryResponse>.ErrorResponse("There is some issue please try after sometime", APIStatusCodes.InternalServerError);
    }

    public async Task<APIResponse<int>> Delete(Guid id)
    {
        var entity = await repository.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

        if (entity is not null)
        {
            var productSolutions= await serviceTypeRepository.FindByAsync(x => x.BrandId == id);
            if (productSolutions.Any())
                   return APIResponse<int>.ErrorResponse("Cannot delete Category Because it is in use!", APIStatusCodes.Conflict);

            int deletedResult = await repository.DeleteAsync(entity);
            if (deletedResult > 0)
            {
                var file = await fileRepository.FirstOrDefaultAsync(x => x.EntityId == id);

                if (file is not null)
                await fileRepository.DeleteAsync(file);
            }

            return APIResponse<int>.SuccessResponse(deletedResult, "Category Deleted Successfully");
        }

        return APIResponse<int>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.NotFound);
    }

    public async Task<APIResponse<IEnumerable<CategoryResponse>>> GetAll()
    {
        var categories = await repository.GetAllCategoriesAsync();
        if (categories.Any())
            return APIResponse<IEnumerable<CategoryResponse>>.SuccessResponse(mapper.Map<IEnumerable<CategoryResponse>>(categories));

        return APIResponse<IEnumerable<CategoryResponse>>.ErrorResponse("No Category found", APIStatusCodes.NotFound);
    }


    public async Task<APIResponse<CategoryResponse>> GetById(Guid id)
    {
        var category = await repository.GetCategoryById(id);

        if (category is null)
            return APIResponse<CategoryResponse>.ErrorResponse("No Category found", APIStatusCodes.NotFound);

        return APIResponse<CategoryResponse>.SuccessResponse(mapper.Map<CategoryResponse>(category));
    }

    public async Task<APIResponse<IEnumerable<CategoryResponse>>> GetManagerCategoriesAsync(Guid id)
    {
        var categories = await repository.GetManagerCategoriesAsync(id);
        if (categories.Any())
            return APIResponse<IEnumerable<CategoryResponse>>.SuccessResponse(categories);

        return APIResponse<IEnumerable<CategoryResponse>>.ErrorResponse("No Category found", APIStatusCodes.NotFound);

    }

    public async Task<bool> IsCategoryUnique(string categoryName)
    {
        return await repository.FirstOrDefaultAsync(x => x.Name == categoryName && x.IsDeleted == false) == null;
    }


    public async Task<APIResponse<CategoryResponse>> Update(UpdateCategoryRequest model)
    {
        var dbCategory = await repository.GetCategoryById(model.Id);

        if (dbCategory is null)
            return APIResponse<CategoryResponse>.ErrorResponse("No Category found", APIStatusCodes.NotFound);

        var categoryExisted = await repository.FirstOrDefaultAsync(x => x.Name == model.Name && x.IsDeleted == false && x.Id != model.Id) != null;

        if (categoryExisted)
            return APIResponse<CategoryResponse>.ErrorResponse("Category already existed", APIStatusCodes.Conflict);

        var category = mapper.Map<Category>(model);
        category.UpdatedBy = contextService.GetUserId();
        category.UpdatedOn = DateTimeOffset.Now;
        var returnResult = await repository.UpdateAsync(category);
      
        if (returnResult > 0)
        {
            var response = mapper.Map<CategoryResponse>(category);
            string path = dbCategory.FilePath;
            if (model.File != null)
            {
               // var dbFileId= await  repository.GetAppFileByEntityId(dbCategory.Id);
                if(!string.IsNullOrEmpty(dbCategory.FilePath))
                {
                    path = await fileService.UploadFileAsync(model.File);

                    var returnCode = await repository.UpdateAppFile(dbCategory.Id, path);
                    if (returnCode > 0)
                        await fileService.DeleteFileAsync(dbCategory.FilePath);
                }
                else
                {
                    path= await fileService.UploadFileAsync(model.File);
                    AppFile file = new()
                    {
                        Id = Guid.NewGuid(),
                        Module = Domain.Enums.Module.Category,
                        CreatedBy = contextService.GetUserId(),
                        CreatedOn = DateTimeOffset.Now,
                        EntityId = category.Id,
                        FilePath = path
                    };
                    var returnCode = await repository.AddAppFile(file);
                }
            }
            response.FilePath = path;
            if (returnResult > 0)
            {
                return APIResponse<CategoryResponse>.SuccessResponse(response,APIStatusCodes.OK,"Category updated Successfully");
            }
        }
        return APIResponse<CategoryResponse>.ErrorResponse("There is some issue please try after sometime", APIStatusCodes.InternalServerError);
    }
}
