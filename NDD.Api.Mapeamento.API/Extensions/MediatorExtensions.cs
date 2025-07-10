using MediatR;

using NDD.Api.Mapeamento.API.Behaviours;

using SimpleInjector;

using System;
using System.Diagnostics.CodeAnalysis;

namespace NDD.Api.Mapeamento.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class MediatorExtensions
    {
        public static void AddMediator(this IServiceCollection services, Container container)
        {
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);

            RegisterAssembly(container, "NDD.Api.Mapeamento.Application");

            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(ValidationPipeline<,>)
            });
        }

        private static void RegisterAssembly(Container container, string assemblyPath)
        {
            var assembly = AppDomain.CurrentDomain.Load(assemblyPath);

            container.Register(typeof(IRequestHandler<,>), assembly);
            container.Register(typeof(IRequestHandler<>), assembly);
        }
    }
}
