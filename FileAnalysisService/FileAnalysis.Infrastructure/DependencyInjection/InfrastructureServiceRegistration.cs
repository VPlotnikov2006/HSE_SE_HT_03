using FileAnalysis.Application.Interfaces;
using FileAnalysis.Domain.Services;
using FileAnalysis.Infrastructure.Clients;
using FileAnalysis.Infrastructure.Persistence;
using FileAnalysis.Infrastructure.SimilarityAlgorithms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileAnalysis.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddFileAnalysisInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<FileAnalysisDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("FileAnalysisDb"));
        });

        services.AddScoped<IPlagiarismReportRepository, EfPlagiarismReportRepository>();
        
        services.AddHttpClient<IFileStorageClient, FileStorageClient>(client =>
        {
            client.BaseAddress = new Uri(
                configuration["Services:FileStorage"] 
                ?? throw new ArgumentNullException(nameof(configuration), "FileStorageAddress is missing")
            );
        });

        services.AddScoped<ISimilarityAlgorithm>(sp =>
        {
            var pow = configuration.GetValue<double>(
                "Similarity:Levenshtein:Power");

            return new LevenshteinDistanceAlgorithm(pow);
        });
        
        return services;
    }
}
