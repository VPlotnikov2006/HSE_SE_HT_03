using FileStorage.Application.UseCases.GetAllFilesForAnalysis;
using FileStorage.Application.UseCases.GetFileForAnalysis;
using FileStorage.Application.UseCases.SaveFile;
using FileStorage.Infrastructure.DependencyInjection;
using FileStorage.Infrastructure.Persistence;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddFileStorageInfrastructure(builder.Configuration);

        builder.Services.AddScoped<GetFileUseCase>();
        builder.Services.AddScoped<GetByWorkIdUseCase>();
        builder.Services.AddScoped<SaveFileUseCase>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<FileStorageDbContext>();
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

