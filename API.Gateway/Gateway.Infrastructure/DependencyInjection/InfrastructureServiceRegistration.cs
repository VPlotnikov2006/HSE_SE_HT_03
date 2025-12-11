using Gateway.Application.Interfaces.Clients;
using Gateway.Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddGatewayInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<IFileStorageClient, FileStorageClient>(client =>
        {
            client.BaseAddress = new Uri(
                configuration["Services:FileStorage"] 
                ?? throw new ArgumentNullException(nameof(configuration), "FileStorageAddress is missing")
            );
        });

        services.AddHttpClient<IFileAnalysisClient, FileAnalysisClient>(client =>
        {
            client.BaseAddress = new Uri(
                configuration["Services:FileAnalysis"] 
                ?? throw new ArgumentNullException(nameof(configuration), "FileAnalysisAddress is missing")
            );
        });

        return services;
    }
}
