using NDD.Api.Mapeamento.Domain.Features.Layout.Contexto;

namespace NDD.Api.Mapeamento.Domain.Features.Layout.Resposta
{
    public interface IMontarRespostaLayout<TContexto, TRequisicao, out TResposta, in TFalha> where TContexto : IContextoLayout<TRequisicao>
    {
        TResposta ProcessarErro(TContexto contexto, TFalha falha);

        TResposta ProcessarSucesso(TContexto contexto);
    }
}
