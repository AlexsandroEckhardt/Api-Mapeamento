using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Contexto;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Modulos.Execucao;
using NDD.Space.Base.Domain;

namespace NDD.Api.Mapeamento.Domain.Features.NomeFeature.Modulos.PersistirDados
{
    public interface IPersistirDadosNomeFeature<TContexto, TRequisicao, TFalha> : IExecucaoNomeFeature<TContexto, Result<TFalha, TContexto>>
                                                                                  where TContexto : IContextoNomeFeature<TRequisicao>
    {

    }
}