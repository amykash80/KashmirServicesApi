using AutoMapper;
using KashmirServices.Application.Abstractions.Identity;
using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;

namespace KashmirServices.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository repository;
        private readonly IMapper mapper;
        private readonly IContextService contextService;

        public BrandService(IBrandRepository repository, IMapper mapper,IContextService contextService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.contextService = contextService;
        }


        public async Task<APIResponse<BrandResponse>> Add(BrandRequest model)
        {
            var brandExisted = await repository.FirstOrDefaultAsync(x=>x.Name == model.Name && x.CategoryId == model.CategoryId && x.IsDeleted == false) != null;

            if (brandExisted)
                return APIResponse<BrandResponse>.ErrorResponse("Brand already added", APIStatusCodes.Conflict);

            var brand=mapper.Map<Brand>(model);
            brand.CreatedBy = contextService.GetUserId();
            var returnResult = await repository.InsertAsync(brand);

            if (returnResult > 0)
            {
                var brandResponse = await repository.GetBrandsById(brand.Id);

                return APIResponse<BrandResponse>.SuccessResponse(brandResponse);
            }
            return APIResponse<BrandResponse>.ErrorResponse("There is some issue please try after sometime", APIStatusCodes.InternalServerError);
        }


        public async Task<bool> IsBrandNameUnique(string brandName)
        {
            return await repository.FirstOrDefaultAsync(x => x.Name == brandName && x.IsDeleted == false) == null;
        }


        public async Task<APIResponse<BrandResponse>> GetById(Guid id)
        {
            var brand = await repository.GetBrandsById(id);

            if (brand is null)
                return APIResponse<BrandResponse>.ErrorResponse("No Brand found", APIStatusCodes.NotFound);

            return APIResponse<BrandResponse>.SuccessResponse(brand);
        }


        public async Task<APIResponse<BrandResponse>> Update(UpdateBrandRequest model)
        {
            var brand = await repository.FirstOrDefaultAsync(x => x.Id == model.Id && x.IsDeleted == false);

            if (brand is null)
                return APIResponse<BrandResponse>.ErrorResponse("No Brand found", APIStatusCodes.NotFound);

            var anyBrand = await repository.FirstOrDefaultAsync(x => x.Name == model.Name && x.CategoryId == model.CategoryId && x.Id != model.Id && x.IsDeleted == false);

            if (anyBrand is not null)
                return APIResponse<BrandResponse>.ErrorResponse("Brand Name Already Exists", APIStatusCodes.Conflict);

            var entity = mapper.Map<Brand>(model);
            entity.UpdatedBy = contextService.GetUserId();
            entity.UpdatedOn =DateTimeOffset.Now;
            int updatedResult = await repository.UpdateAsync(entity);

            if (updatedResult > 0)
            {
                var brandResponse = await repository.GetBrandsById(entity.Id);
                return APIResponse<BrandResponse>.SuccessResponse(brandResponse);
            }

            return APIResponse<BrandResponse>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
        }


        public async Task<APIResponse<BrandResponse>> Delete(Guid id)
        {
            var dbBrand = await repository.GetByIdAsync(id);

            if (dbBrand is null)
                return APIResponse<BrandResponse>.ErrorResponse("No Brand found", APIStatusCodes.NotFound);

            dbBrand.IsDeleted = true;
            dbBrand.UpdatedOn = DateTimeOffset.Now;
            dbBrand.UpdatedBy=contextService.GetUserId();
            int returnValue = await repository.UpdateAsync(dbBrand);

            if (returnValue > 0)
                return APIResponse<BrandResponse>.SuccessResponse(mapper.Map<BrandResponse>(dbBrand),APIStatusCodes.OK,"Brand Deleted Successfully");

            return APIResponse<BrandResponse>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
        }


        public async Task<APIResponse<IEnumerable<BrandResponse>>> GetAll()
        {
            var brands = await repository.GetAllBrands();
            if (brands.Any())
                return APIResponse<IEnumerable<BrandResponse>>.SuccessResponse(brands);

            return APIResponse<IEnumerable<BrandResponse>>.ErrorResponse("No Brand found", APIStatusCodes.NotFound);
        }


        public async Task<APIResponse<IEnumerable<BrandNames>>> GetBrandNames()
        {
            var brands = await repository.FindByAsync(x => x.IsDeleted == false);
            if (brands.Any())
                return APIResponse<IEnumerable<BrandNames>>.SuccessResponse(mapper.Map<IEnumerable<BrandNames>>(brands));

            return APIResponse<IEnumerable<BrandNames>>.ErrorResponse("No Brand found", APIStatusCodes.NotFound);
        }

        public async Task<APIResponse<IEnumerable<BrandResponse>>> GetByCategoryId(Guid id)
        {
            var brands = await repository.GetByCategoryId(id);
            if (brands.Any())
                return APIResponse<IEnumerable<BrandResponse>>.SuccessResponse(brands);

            return APIResponse<IEnumerable<BrandResponse>>.ErrorResponse("No Brand found", APIStatusCodes.NotFound);
        }

    }
}
