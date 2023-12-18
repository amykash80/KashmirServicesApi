using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace KashmirServices.Persistence.Repositories;

internal sealed class PartsRepository : BaseRepository<Parts>, IPartsRepository
{
    private readonly KashmirServicesDbContext context;

    public PartsRepository(KashmirServicesDbContext context) : base(context)
    {
        this.context = context;
    }
}
