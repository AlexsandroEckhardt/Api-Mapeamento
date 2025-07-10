using Microsoft.AspNetCore.Mvc.Controllers;

using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

using System.Diagnostics.CodeAnalysis;

namespace NDD.Api.Mapeamento.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SimpleInjectorExtensions
    {
        public static void AddSimpleInjector(this IServiceCollection services, Container container)
        {
            // Define que a instância vai existir dentro de um determinado escopo (implícito ou explícito).
            // Assume o fluxo de controle dos métodos assíncronos automaticamente
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));

            // Ao invocar o método de extensão UseSimpleInjectorAspNetRequestScoping, o tempo que uma 
            // requisição está ativa é o período que um escopo vai estar ativo. 
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        public static void AdicionarDependenciasBanco(this IServiceCollection services, IConfiguration configuration)
        {
            Infra.Data.Inicializacao.AdicionarDependencias(services, configuration);
        }
    }
}
