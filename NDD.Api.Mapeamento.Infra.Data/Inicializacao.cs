using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NDD.Api.Mapeamento.Base.Configuracoes;
using NDD.Api.Mapeamento.Domain.Features.Layout.Repositorio;
using NDD.Api.Mapeamento.Infra.Data.Contexts;
using NDD.Api.Mapeamento.Infra.Data.Feature;
using NDD.Space.Base.Extensions;

namespace NDD.Api.Mapeamento.Infra.Data
{
    public static class Inicializacao
    {
        public static void AdicionarDependencias(IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.LoadSettings<AppSettingsConfiguracao>("AppSettingsConfiguracao", services);

            services.AddScoped((ctx) =>
            {
                var options = new DbContextOptionsBuilder<TemplateDbContext>()
                                 .UseSqlServer(appSettings.ConnectionString,
                                  opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)).Options;

                return new TemplateDbContext(options);
            });

            services.AddTransient<IRepositorioLayout, LayoutRepository>();
        }
    }
}