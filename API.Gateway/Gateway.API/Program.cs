using Gateway.Application.UseCases;
using Gateway.Infrastructure.DependencyInjection;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<SaveFileUseCase>();
        builder.Services.AddScoped<GetReportsUseCase>();
        builder.Services.AddScoped<GenerateWordCloudUseCase>();

        builder.Services.AddGatewayInfrastructure(builder.Configuration);

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway API");
        });

        app.MapGet("/swagger/all/ui", ctx =>
        {
            ctx.Response.Redirect("/swagger/all/ui/index.html");
            return Task.CompletedTask;
        });

        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = "swagger/all/ui";
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway API");

            var services = builder.Configuration.GetSection("Services").Get<Dictionary<string, string>>();
            if (services != null)
            {
                foreach (var entry in services)
                {
                    c.SwaggerEndpoint($"/swagger/all/remote/{entry.Key}/swagger.json", entry.Key);
                }
            }
        });
        
        app.MapControllers();

        app.Run();
    }
}
