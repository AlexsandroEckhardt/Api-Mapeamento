using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Contexto;

namespace NDD.Api.Mapeamento.Domain.Features.NomeFeature.Resposta
{
    public interface IMontarRespostaNomeFeature<TContexto, TRequisicao, out TResposta, in TFalha> where TContexto : IContextoNomeFeature<TRequisicao>
    {
        TResposta ProcessarErro(TContexto contexto, TFalha falha);

        TResposta ProcessarSucesso(TContexto contexto);
    }
}
