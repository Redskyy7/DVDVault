using DVDVault.Application.Abstractions.Director;
using DVDVault.Application.Abstractions.DVDs;
using DVDVault.Application.UseCases.Directors.Handler;
using DVDVault.Application.UseCases.DVDs.Handler;
using DVDVault.Domain.Interfaces.Repositories;
using DVDVault.Domain.Interfaces.Services;
using DVDVault.Domain.Interfaces.UnitOfWork;
using DVDVault.Infra.Caching;
using DVDVault.Infra.Data.Context;
using DVDVault.Infra.Repositories;
using DVDVault.Infra.Services;
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
        AddDVDHandlers(builder);
        AddRepository(builder);
        AddServices(builder);
    }

    private static void AddDirectorHandlers(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICreateDirectorHandler, CreateDirectorHandler>();
        builder.Services.AddScoped<IUpdateDirectorNameHandler, UpdateDirectorNameHandler>();
        builder.Services.AddScoped<IUpdateDirectorSurnameHandler, UpdateDirectorSurnameHandler>();
        builder.Services.AddScoped<ISoftDeleteDirectorHandler, SoftDeleteDirectorHandler>();
        builder.Services.AddScoped<IHardDeleteDirectorHandler, HardDeleteDirectorHandler>();
    }

    private static void AddDVDHandlers(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICreateDVDHandler, CreateDVDHandler>();
        builder.Services.AddScoped<IUpdateDVDTitleHandler, UpdateDVDTitleHandler>();
        builder.Services.AddScoped<ISoftDeleteDVDHandler, SoftDeleteDVDHandler>();
        builder.Services.AddScoped<IHardDeleteDVDHandler, HardDeleteDVDHandler>();
        builder.Services.AddScoped<IRentCopyHandler, RentCopyHandler>();
        builder.Services.AddScoped<IReturnCopyHandler, ReturnCopyHandler>();
    }

    private static void AddServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IDVDService, DVDService>();
        builder.Services.AddScoped<ICachingService, CachingService>();
    }

    private static void AddRepository(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(serviceType: typeof(IBaseRepository<>), implementationType: typeof(BaseRepository<>));
        builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();
        builder.Services.AddScoped<IDVDRepository, DVDRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
