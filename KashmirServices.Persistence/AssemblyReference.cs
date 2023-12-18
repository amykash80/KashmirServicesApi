using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KashmirServices.Persistence;

public static partial class AssemblyReference
{
    private const string KashmirServiceConnection = "KashmirServicesConnection";
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<KashmirServicesDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(KashmirServiceConnection));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICallBookingRepository, CallBookingRepository>();
        services.AddScoped<IEngineerRepository, EngineerRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        services.AddScoped<IJobRoleRepository, JobRoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPartsRepository, PartsRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IVisitRepository, VisitRepository>();

        return services;
    }
}