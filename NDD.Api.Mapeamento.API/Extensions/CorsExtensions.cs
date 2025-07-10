using NDD.Space.Base.CORS;
using NDD.Space.Base.Extensions;

using System.Diagnostics.CodeAnalysis;

namespace NDD.Api.Mapeamento.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class CorsExtensions
    {

        public static void UseCORS(this IApplicationBuilder app, IConfiguration configuration)
        {
            var corsSettings = configuration.LoadSettings<CORSSettings>("CORSSettings") ?? new CORSSettings().Default();

            app.UseCors(builder => builder
                                   .WithOrigins(corsSettings.Origins)
                                   .WithMethods(corsSettings.Methods)
                                   .WithHeaders(corsSettings.Headers)
                                   .Build());
        }
    }
}
