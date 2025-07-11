using Microsoft.Extensions.DependencyInjection;

using NDD.Api.Mapeamento.Domain.Features.Layout.Contexto;
using NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.Execucao;
using NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.MontarInformacoes;
using NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.PersistirDados;
using NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.ValidarNegocio;
using NDD.Api.Mapeamento.Domain.Features.Layout.Requisicao;
using NDD.Api.Mapeamento.Domain.Features.Layout.Resposta;
using NDD.Space.Base.Domain.Resposta;

namespace NDD.Api.Mapeamento.Domain
{
    public static class Inicializacao
    {
        public static IServiceCollection InicializarDomain(this IServiceCollection services)
        {
            AdicionarDependenciasLayout(services);

            return services;
        }

        private static void AdicionarDependenciasLayout(IServiceCollection services)
        {
            services.AddTransient<IExecucaoLayout<LayoutRequisicao, LayoutResposta>, ExecucaoLayoutImpl<LayoutContexto, LayoutRequisicao, LayoutResposta, RespostaDeRequisicao>>();
            services.AddTransient<IValidarNegocioLayout<LayoutContexto, LayoutRequisicao, RespostaDeRequisicao>, ValidarNegocioLayoutImpl>();
            services.AddTransient<IPersistirDadosLayout<LayoutContexto, LayoutRequisicao, RespostaDeRequisicao>, PersistirDadosLayoutImpl>();
            services.AddTransient<IMontarInformacoesLayout<LayoutContexto, LayoutRequisicao, RespostaDeRequisicao>, MontarInformacoesLayoutImpl>();
            services.AddTransient<IMontarRespostaLayout<LayoutContexto, LayoutRequisicao, LayoutResposta, RespostaDeRequisicao>, MontarRespostaLayoutImpl>();
        }
    }
}
