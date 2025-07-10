

using FluentMigrator.Runner;
using FluentMigrator.Runner.Conventions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NDD.Api.Mapeamento.Base.Configuracoes;
using NDD.Api.Mapeamento.Base.Constantes;
using NDD.Space.Base.Extensions;

using System.Reflection;

namespace NDD.Api.Mapeamento.Infra.Migracoes
{
    public static class Inicializacao
    {
        public static void InicializarMigracoes(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.LoadSettings<AppSettings>("AppSettings", services);

            var serviceProvider = services.AddFluentMigratorCore()
                     .ConfigureRunner(rb => rb
                     .AddSqlServer()
                     .WithGlobalCommandTimeout(TimeSpan.FromMinutes(60))
                     .WithGlobalConnectionString(appSettings.ConnectionString)
                     .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations()
                     .ScanIn(Assembly.GetExecutingAssembly()).For.EmbeddedResources()
                 )
                 .AddSingleton<IConventionSet>(new DefaultConventionSet(TemplateConst.NomeSchema, null))
                 .BuildServiceProvider(false);

            using (var scope = serviceProvider.CreateScope())
            {
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

                runner.MigrateUp();
            }
        }
    }
}
