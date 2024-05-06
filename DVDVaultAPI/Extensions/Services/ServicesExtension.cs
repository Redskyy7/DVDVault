using DVDVault.Application.Abstractions.Director;
using DVDVault.Application.UseCases.Directors.Handler;
using DVDVault.Domain.Interfaces.Repositories;
using DVDVault.Domain.Interfaces.UnitOfWork;
using DVDVault.Infra.Data.Context;
using DVDVault.Infra.Repositories;
using DVDVault.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DVDVault.WebAPI.Extensions.Services;

public static class ServicesExtension
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {   
        string connection = configuration.GetConnectionString("Database") ?? throw new ConnectionNotDefinedException($"Connection not defined.");

        services.AddDbContext<DVDVaultContext>(options => options.UseNpgsql(connection));
    }

    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        AddDirectorHandlers(builder);
        AddRepository(builder);
    }

    private static void AddDirectorHandlers(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICreateDirectorHandler, CreateDirectorHandler>();
        builder.Services.AddScoped<IUpdateDirectorNameHandler, UpdateDirectorNameHandler>();
        builder.Services.AddScoped<IUpdateDirectorSurnameHandler, UpdateDirectorSurnameHandler>();
        builder.Services.AddScoped<ISoftDeleteDirectorHandler, SoftDeleteDirectorHandler>();
        builder.Services.AddScoped<IHardDeleteDirectorHandler, HardDeleteDirectorHandler>();
    }

    private static void AddRepository(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(serviceType: typeof(IBaseRepository<>), implementationType: typeof(BaseRepository<>));
        builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
