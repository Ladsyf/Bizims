using Bizims.Api.Extensions;
using Bizims.Api.Middlewares;
using Bizims.Application;
using Bizims.Infrastructure;

namespace Bizims.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen();

        builder.Services.AddHttpContextAccessor();

        builder.AddCustomSettings();
        builder.AddJwtBearerAuthentication();
        builder.AddCors();

        builder.Services.AddApplicationDI();
        builder.Services.AddInfrastructureDI();

        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseDefaultFiles();
        app.MapStaticAssets();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseJwtMiddleware();
        app.UseUserMiddleware();

        app.MapControllers();

        app.Run();
    }
}
