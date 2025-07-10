using Microsoft.Extensions.DependencyInjection;

using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Contexto;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Modulos.Execucao;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Modulos.MontarInformacoes;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Modulos.PersistirDados;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Modulos.ValidarNegocio;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Requisicao;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Resposta;
using NDD.Space.Base.Domain.Resposta;

namespace NDD.Api.Mapeamento.Domain
{
    public static class Inicializacao
    {
        public static IServiceCollection InicializarDomain(this IServiceCollection services)
        {
            AdicionarDependenciasNomeFeature(services);

            return services;
        }

        private static void AdicionarDependenciasNomeFeature(IServiceCollection services)
        {
            services.AddTransient<IExecucaoNomeFeature<NomeFeatureRequisicao, NomeFeatureResposta>, ExecucaoNomeFeatureImpl<NomeFeatureContexto, NomeFeatureRequisicao, NomeFeatureResposta, RespostaDeRequisicao>>();
            services.AddTransient<IValidarNegocioNomeFeature<NomeFeatureContexto, NomeFeatureRequisicao, RespostaDeRequisicao>, ValidarNegocioNomeFeatureImpl>();
            services.AddTransient<IPersistirDadosNomeFeature<NomeFeatureContexto, NomeFeatureRequisicao, RespostaDeRequisicao>, PersistirDadosNomeFeatureImpl>();
            services.AddTransient<IMontarInformacoesNomeFeature<NomeFeatureContexto, NomeFeatureRequisicao, RespostaDeRequisicao>, MontarInformacoesNomeFeatureImpl>();
            services.AddTransient<IMontarRespostaNomeFeature<NomeFeatureContexto, NomeFeatureRequisicao, NomeFeatureResposta, RespostaDeRequisicao>, MontarRespostaNomeFeatureImpl>();
        }
    }
}
