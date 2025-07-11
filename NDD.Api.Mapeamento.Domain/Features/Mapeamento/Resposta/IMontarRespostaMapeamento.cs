using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Contexto;

namespace NDD.Api.Mapeamento.Domain.Features.Mapeamento.Resposta
{
    public interface IMontarRespostaMapeamento<TContexto, TRequisicao, out TResposta, in TFalha> where TContexto : IContextoMapeamento<TRequisicao>
    {
        TResposta ProcessarErro(TContexto contexto, TFalha falha);

        TResposta ProcessarSucesso(TContexto contexto);
    }
}
