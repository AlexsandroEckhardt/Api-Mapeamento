using NDD.MicroServico.Base.Negocio;

namespace NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.Execucao
{
    public interface IExecucaoLayout<TRequisicao, TResposta> : IRequisicaoRNAComResposta<TRequisicao, TResposta>
    {
        new TResposta Processar(TRequisicao requisicao);
    }
}
