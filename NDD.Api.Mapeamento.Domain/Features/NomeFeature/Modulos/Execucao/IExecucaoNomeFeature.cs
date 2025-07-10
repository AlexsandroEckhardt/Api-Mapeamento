using NDD.MicroServico.Base.Negocio;

namespace NDD.Api.Mapeamento.Domain.Features.NomeFeature.Modulos.Execucao
{
    public interface IExecucaoNomeFeature<TRequisicao, TResposta> : IRequisicaoRNAComResposta<TRequisicao, TResposta>
    {
        new TResposta Processar(TRequisicao requisicao);
    }
}
