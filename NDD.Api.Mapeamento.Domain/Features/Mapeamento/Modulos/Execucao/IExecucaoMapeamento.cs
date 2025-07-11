using NDD.MicroServico.Base.Negocio;

namespace NDD.Api.Mapeamento.Domain.Features.Mapeamento.Modulos.Execucao
{
    public interface IExecucaoMapeamento<TRequisicao, TResposta> : IRequisicaoRNAComResposta<TRequisicao, TResposta>
    {
        new TResposta Processar(TRequisicao requisicao);
    }
}
