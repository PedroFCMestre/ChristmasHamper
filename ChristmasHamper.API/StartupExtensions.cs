using ChristmasHamper.API.Services;
using ChristmasHamper.Application;
using ChristmasHamper.Application.Contracts;
using ChristmasHamper.Infrastructure;
using ChristmasHamper.Persistence;
using Microsoft.OpenApi.Models;

namespace ChristmasHamper.API;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        AddSwagger(builder.Services);

        //configure application layer services
        builder.Services.AddApplicationServices();

        //configure persistence layer services
        builder.Services.AddPersistenceServices(builder.Configuration);

        //adds LoggedInUserService
        builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

        
        builder.Services.AddHttpContextAccessor();

        //adds services and routing logic so controllers can handle requests
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        /*builder.Services.AddCors(
            options => options.AddPolicy(
                "open",
                policy => policy.WithOrigins([builder.Configuration["ApiUrl"] ?? "https://localhost:7020", 
                                              builder.Configuration["BlazorUrl"] ?? "https://localhost:7080"])
                .AllowAnyMethod()
                .SetIsOriginAllowed(pol => true)
                .AllowAnyHeader()
                .AllowCredentials()));*/


        //configure infrastructure layer hostBuilder
        builder.Host.AddInfrastructureHostBuilder();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseCors("open");

        //intercepts http requests and redirects them to https (encrypts the transmitted data)
        //comes early to force other middleware to be accessed over a secure connection
        app.UseHttpsRedirection();

        //app.UseAuthorization();

        //maps http requests to actions on controllers that are decorated with the [ApiController] attribute
        app.MapControllers();

        return app;
    }

    public static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1.0",
                Title = "ChristmasHamper API",

            });
        });
    }
}

