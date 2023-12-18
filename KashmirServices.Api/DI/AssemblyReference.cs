using KashmirServices.Api.Middlewares;
using KashmirServices.Application;
using KashmirServices.Infrastructure;
using KashmirServices.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace KashmirServices.Api.DI;

public static partial class AssemblyReference
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kashmir Service API", Version = "v1" });
        });
        services.AddHttpContextAccessor();

        services.AddApplicationServices(environment.WebRootPath)
        .AddInfrastructureServices(configuration)
        .AddPersistenceServices(configuration);

        services.AddCors();

        services.AddAuthentication(options =>
        {

            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,

                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });

        services.AddLogging();

        services.AddTransient<GlobalExceptionHandlingMiddleware>(); 

        return services;
    }
}
