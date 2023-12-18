using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Domain.Entities;

namespace KashmirServices.Persistence.Repositories;

internal class ContactRepository:BaseRepository<Contact>, IContactRepository
{
    public ContactRepository(KashmirServicesDbContext context) : base(context)
    {
            
    }
}
