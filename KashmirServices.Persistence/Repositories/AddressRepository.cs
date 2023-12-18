using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Domain.Entities;

namespace KashmirServices.Persistence.Repositories;

public class AddressRepository : BaseRepository<Address>, IAddressRepository
{
    public AddressRepository(KashmirServicesDbContext context) : base(context)
    {
    }
}
