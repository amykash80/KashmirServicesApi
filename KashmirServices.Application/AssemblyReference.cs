using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KashmirServices.Application;
public static partial class AssemblyReference
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, string webrootPath)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddSingleton<IFileService>(new FileService(webrootPath));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IServiceTypeService, ServiceTypeService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICallBookingService, CallBookingService>();
        services.AddScoped<IEngineerService, EngineerService>();
        services.AddScoped<IManagerService, ManagerService>();
        services.AddScoped<IJobRoleService, JobRoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPartsService, PartsService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IVisitService, VisitService>();



        return services;
    }
}
