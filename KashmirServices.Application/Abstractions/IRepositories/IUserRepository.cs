using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.Abstractions.IRepositories;

public interface IUserRepository : IBaseRepository<User>
{
     Task<IEnumerable<UserResponse>> GetAllUsers(string searchTerm, UserStatus status, UserRole userRole);

    Task<UserResponse> GetUserById(Guid id);
}
