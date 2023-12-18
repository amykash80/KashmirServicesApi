using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.Abstractions.IServices;

public interface IUserService
{
    Task<APIResponse<IEnumerable<UserResponse>>> GetAllUsers(string searchTerm, UserStatus status, UserRole userRole);

    Task<APIResponse<UserResponse>> GetUserById(Guid? id);


    Task<APIResponse<UserResponse>> UpdateUser(UserUpdateRequest model);


    Task<APIResponse<string>> UpdateUserStatus(Guid id);


    Task<APIResponse<IEnumerable<ManagerBasicDetailsResponse>>> GetAllManagerBasicDetails();

    Task<APIResponse<IEnumerable<ServiceEngineerBasicDetailsResponse>>> GetAllEngineersBasicDetails();

}
