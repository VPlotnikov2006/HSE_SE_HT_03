using FileStorage.Application.Interfaces;
using FileStorage.Infrastructure.FileProviders.LocalFileProvider;
using FileStorage.Infrastructure.Options;
using FileStorage.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileStorage.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddFileStorageInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<FileStorageOptions>(configuration.GetSection("FileStorage"));

        services.AddDbContext<FileStorageDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("FileStorageDb"));
        });

        services.AddScoped<IFileRepository, EfFileMetadataRepository>();

        services.AddScoped<IFileProvider, LocalFileProvider>();

        return services;
    }
}
