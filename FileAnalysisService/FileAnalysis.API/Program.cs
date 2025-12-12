using FileAnalysis.Application.UseCases.AnalyseFileUseCase;
using FileAnalysis.Application.UseCases.GenerateWordCloud;
using FileAnalysis.Application.UseCases.GetReportsUseCase;
using FileAnalysis.Infrastructure.DependencyInjection;
using FileAnalysis.Infrastructure.Persistence;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddFileAnalysisInfrastructure(builder.Configuration);

        builder.Services.AddScoped<AnalyseFileUseCase>();
        builder.Services.AddScoped<GetReportsUseCase>();
        builder.Services.AddScoped<GenerateWordCloudUseCase>();

        var app = builder.Build();
        app.UseCors();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<FileAnalysisDbContext>();
            db.Database.EnsureCreated();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}