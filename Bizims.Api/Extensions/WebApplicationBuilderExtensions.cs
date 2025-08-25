using Bizims.Application.Helpers;
using Bizims.Application.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Bizims.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddCustomSettings(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddOptions<CookieSettings>()
            .BindConfiguration(nameof(CookieSettings));

        builder.Services
            .AddOptions<CorsSettings>()
            .BindConfiguration(nameof(CorsSettings));

        builder.Services
            .AddOptions<DatabaseSettings>()
            .BindConfiguration(nameof(DatabaseSettings));

        builder.Services
            .AddOptions<JwtSettings>()
            .BindConfiguration(nameof(JwtSettings));

        return builder;
    }

    public static WebApplicationBuilder AddJwtBearerAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtSettings = builder.Configuration
                    .GetRequiredSection(nameof(JwtSettings))
                    .Get<JwtSettings>()!;

                var tokenValidationParameters = TokenValidationParametersHelper.Create(jwtSettings);

                options.TokenValidationParameters = tokenValidationParameters;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                var cookieSettings = builder.Configuration
                    .GetSection(nameof(CookieSettings))
                    .Get<CookieSettings>()!;

                options.Cookie.Name = CookieSettings.AccessTokenKey;
                options.Cookie.HttpOnly = cookieSettings.HttpOnly;
                options.Cookie.SecurePolicy = cookieSettings.CookieSecurePolicy;
                options.Cookie.SameSite = cookieSettings.SameSiteMode;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(cookieSettings.JwtExpiryInMinutes);
                options.SlidingExpiration = true;
            });

        return builder;
    }

    public static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            var corsSettings = builder.Configuration
                .GetRequiredSection(nameof(CorsSettings))
                .Get<CorsSettings>()!;

            options.AddPolicy(nameof(CorsSettings), builder =>
            {
                builder.WithOrigins(corsSettings.Origins)
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return builder;
    }
}