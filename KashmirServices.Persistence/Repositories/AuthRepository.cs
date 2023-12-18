using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Domain.Entities;

namespace KashmirServices.Persistence.Repositories;

public class AuthRepository : BaseRepository<User>, IAuthRepository
{
    public AuthRepository(KashmirServicesDbContext context) : base(context)
    {
    }
}

