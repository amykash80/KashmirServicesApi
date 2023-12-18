using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KashmirServices.Persistence.Repositories;

internal sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly KashmirServicesDbContext context;

    public UserRepository(KashmirServicesDbContext context) : base(context)
    {
        this.context = context;
    }

    private readonly string baseQuery = $@"SELECT U.Id, U.FullName, U.Username ,U.Email, U.PhoneNumber, U.Gender, U.UserRole,
					                        U.UserStatus, U.IsEmailVerified, U.CreatedOn, F.FilePath, F.Module  
					                        FROM Users U
                                            LEFT JOIN AppFiles F
                                            ON U.Id = F.EntityId ";


    public async Task<IEnumerable<UserResponse>> GetAllUsers(string searchTerm, UserStatus status, UserRole userRole)
    {
        return await QueryAsync<UserResponse>(baseQuery + $"  WHERE U.UserRole  = {(int)userRole} AND U.UserStatus ={(int)status} AND ((U.FullName LIKE '{searchTerm}%') OR ( U.PhoneNumber LIKE '{searchTerm}%')) ", null);
    }

    public async Task<UserResponse> GetUserById(Guid id)
    {
        return await FirstOrDefaultAsync<UserResponse>(baseQuery +" Where U.Id =@id ", new { id });
    }
}
