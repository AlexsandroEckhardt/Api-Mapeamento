using log4net;
using Microsoft.AspNetCore.HttpOverrides;

using NDD.MicroServico.Base;
using NDD.MicroServico.Base.Filas;
using NDD.MicroServico.Base.ProvedorServico.MSDI;
using NDD.MicroServico.Base.Serilog;
using NDD.Api.Mapeamento.API.Extensions;
using NDD.Api.Mapeamento.Base.Configuracoes;
using NDD.Api.Mapeamento.Domain;
using NDD.Space.Base.Throttling;

using NServiceBus.Logging;
using NServiceBus.SimpleInjector;

using SimpleInjector;

using System.Diagnostics.CodeAnalysis;

using LogManager = NServiceBus.Logging.LogManager;

namespace NDD.Api.Mapeamento.API
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private static Container Container { get; } = new Container();

        private static readonly object _lockObject = new object();

        private readonly IConfiguration _configuracoes;

        private readonly GerenciamentoConfiguracoes _configuracoesApi;

        public Startup(IConfiguration configuration)
        {
            _configuracoes = configuration;

            _configuracoesApi = new GerenciamentoConfiguracoes();

            configuration.Bind(_configuracoesApi);

            Container.Options.AllowOverridingRegistrations = true;
            Container.Options.AutoWirePropertiesImplicitly();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            LogicalThreadContext.Properties["threadid"] = Thread.CurrentThread.ManagedThreadId;

            services.AddMVC();
            services.AddCors();

            services.AddSimpleInjector(Container, Options =>
            {
                Options.AddAspNetCore().AddControllerActivation();
            });

            services.AddAuth(_configuracoes);
            services.AddMediator(Container);
            services.AddValidators(Container);
            services.AddFilters();
            services.AddSwagger();
            services.AdicionarMiddleware();
            services.AdicionarSerilog(_configuracoesApi.CaminhoArquivoLog);
            services.AdicionarServicosBaseLog();
            services.AddHttpClient();
            services.AddAutoMapper();
            services.AdicionarProvedorServicoMSDI();
            services.InicializarDomain();

            services.AdicionarDependenciasBanco(_configuracoes);

            services.AddTransient(c => _configuracoes);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            app.UseMvc();

            app.UseCORS(_configuracoes);

            app.ConfigSwagger();

            app.UseStaticFiles();
        }
    }
}