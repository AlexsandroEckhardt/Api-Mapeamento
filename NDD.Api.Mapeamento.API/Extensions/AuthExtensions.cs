using Microsoft.AspNetCore.Authentication.JwtBearer;

using NDD.Api.Mapeamento.Base.Configuracoes;
using NDD.Space.Base.Extensions;

using System.Diagnostics.CodeAnalysis;

namespace NDD.Api.Mapeamento.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class AuthExtensions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var authSettings = configuration.LoadSettings<AuthSettings>("AuthSettings", services);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.Authority = authSettings.AuthorityIdentity;
                    jwtBearerOptions.Audience = authSettings.AudienceIdentity;
                });
        }

    }
}
