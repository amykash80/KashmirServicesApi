using AutoMapper;
using KashmirServices.Application.Abstractions.Identity;
using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository repository;
    private readonly IContextService contextService;
    private readonly IFileService fileService;
    private readonly IBaseRepository<AppFile> fileRepository;
    private readonly IMapper mapper;

    public UserService(
        IUserRepository repository,
        IContextService contextService,
        IFileService fileService,
        IBaseRepository<AppFile> fileRepository,
        IMapper mapper)
    {
        this.repository = repository;
        this.contextService = contextService;
        this.fileService = fileService;
        this.fileRepository = fileRepository;
        this.mapper = mapper;
    }

    public async Task<APIResponse<IEnumerable<ManagerBasicDetailsResponse>>> GetAllManagerBasicDetails()
    {
        var managers = await repository.FindByAsync(x => x.UserRole == UserRole.Manager);

        return APIResponse<IEnumerable<ManagerBasicDetailsResponse>>.SuccessResponse(
            managers.Select(x => new ManagerBasicDetailsResponse(x!.Id, x.FullName)));
    }
  

    public async Task<APIResponse<IEnumerable<ServiceEngineerBasicDetailsResponse>>> GetAllEngineersBasicDetails()
    {
        var engineers = await repository.FindByAsync(x => x.UserRole == UserRole.ServiceEngineer);

        return APIResponse<IEnumerable<ServiceEngineerBasicDetailsResponse>>.SuccessResponse(
            engineers.Select(x => new ServiceEngineerBasicDetailsResponse { Id= x!.Id, FullName= x.FullName }));
    }


    public async Task<APIResponse<IEnumerable<UserResponse>>> GetAllUsers(string searchTerm, UserStatus status, UserRole userRole)
    {
        var userResponse = await repository.GetAllUsers(searchTerm, status, userRole);
        return APIResponse<IEnumerable<UserResponse>>.SuccessResponse(userResponse);
    }


    public async Task<APIResponse<UserResponse>> GetUserById(Guid? id)
    {
        var userId = id ?? contextService.GetUserId()!.Value;
        var userResponse = await repository.GetUserById(userId);
        if (userResponse is null)
            return APIResponse<UserResponse>.ErrorResponse(APIMessages.NotFound, APIStatusCodes.NotFound);

        return APIResponse<UserResponse>.SuccessResponse(userResponse);
    }


    public async Task<APIResponse<UserResponse>> UpdateUser(UserUpdateRequest model)
    {
        var user = await repository.GetByIdAsync(model.Id);
        if (user is null)
            return APIResponse<UserResponse>.ErrorResponse(APIMessages.NotFound, APIStatusCodes.NotFound);

        var emailExist = await repository.FirstOrDefaultAsync(x => x.Email == model.Email && x.Id != model.Id) is not null;

        if (emailExist)
            return APIResponse<UserResponse>.ErrorResponse("Email already exists please choose another", APIStatusCodes.Conflict);

        var phoneExist = await repository.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber && x.Id != model.Id) is not null;

        if (phoneExist)
            return APIResponse<UserResponse>.ErrorResponse("PhoneNo already exists please choose another", APIStatusCodes.Conflict);

        var updatedUser = mapper.Map(model, user);

        int returnValue = await repository.UpdateAsync(updatedUser);

        if (returnValue > 0)
        {
            if (model.File != null)
            {
                var dbAppFile = await fileRepository.FirstOrDefaultAsync(x => x.EntityId == updatedUser.Id);
                if (dbAppFile != null)
                {
                    string oldPath = dbAppFile.FilePath;

                    dbAppFile.FilePath = await fileService.UploadFileAsync(model.File);

                    var returnCode = await fileRepository.UpdateAsync(dbAppFile);
                    if (returnCode > 0)
                        await fileService.DeleteFileAsync(oldPath);
                }
                else
                {
                    string path = await fileService.UploadFileAsync(model.File);
                    AppFile file = new()
                    {
                        Module = Domain.Enums.Module.User,
                        CreatedBy = contextService.GetUserId(),
                        CreatedOn = DateTimeOffset.Now,
                        EntityId = updatedUser.Id,
                        FilePath = path
                    };
                    var returnCode = await fileRepository.InsertAsync(file);
                }
            }
            var userResponse = await repository.GetUserById(updatedUser.Id);
            return APIResponse<UserResponse>.SuccessResponse(userResponse, "User updated successfully");

        }
        return APIResponse<UserResponse>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
    }


    public async Task<APIResponse<string>> UpdateUserStatus(Guid id)
    {
        var user = await repository.GetByIdAsync(id);

        if (user is null) return APIResponse<string>.ErrorResponse(APIMessages.NotFound, APIStatusCodes.NotFound);

        user.UserStatus = user.UserStatus == UserStatus.Active ? UserStatus.InActive : UserStatus.Active;

        var updateResult = await repository.UpdateAsync(user);
        if (updateResult > 0) return APIResponse<string>.SuccessResponse(APIMessages.UpdatedSuccessfully);

        return APIResponse<string>.ErrorResponse();
    }
}
