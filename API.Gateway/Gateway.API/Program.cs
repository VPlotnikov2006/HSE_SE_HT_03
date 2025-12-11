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

        builder.Services.AddGatewayInfrastructure(builder.Configuration);

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapControllers();

        app.Run();
    }
}
