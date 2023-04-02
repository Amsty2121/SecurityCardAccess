using Application.Configurations;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, AuthOptions authOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),

                    RoleClaimType = "Roles"
                };
            });
        }

        public static void AddJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(config =>
            {
                config.DefaultPolicy = new AuthorizationPolicyBuilder()
                                        .RequireAuthenticatedUser()
                                        .RequireClaim(nameof(User.Id))
                                        .Build();
            });
        }

        public static AuthOptions ConfigureAuthOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var authOptionsConfigurationSection = configuration.GetSection("AuthOptions");
            services.Configure<AuthOptions>(authOptionsConfigurationSection);
            var authOptions = authOptionsConfigurationSection.Get<AuthOptions>();

            return authOptions;
        }

        public static SyncServiceSettings ConfigureSyncServiceOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var syncServiceConfigurationSection = configuration.GetSection("SyncServiceRoute");
            services.Configure<SyncServiceSettings>(syncServiceConfigurationSection);
            var syncServiceSettings = syncServiceConfigurationSection.Get<SyncServiceSettings>();

            return syncServiceSettings;
        }
    }
}