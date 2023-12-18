using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Domain.Entities;

namespace KashmirServices.Persistence.Repositories;

internal sealed class ServiceTypeRepository : BaseRepository<ServiceType>, IServiceTypeRepository
{
    public ServiceTypeRepository(KashmirServicesDbContext context) : base(context)
    {
    }
}