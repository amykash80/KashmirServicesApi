using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace KashmirServices.Persistence.Repositories;

internal sealed class VisitRepository : BaseRepository<Visit>, IVisitRepository
{
    private readonly KashmirServicesDbContext context;

    public VisitRepository(KashmirServicesDbContext context) : base(context)
    {
        this.context = context;
    }
 
}
