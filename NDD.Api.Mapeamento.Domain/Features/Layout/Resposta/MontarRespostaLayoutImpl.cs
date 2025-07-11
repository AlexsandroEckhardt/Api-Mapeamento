using NDD.Api.Mapeamento.Domain.Features.Layout.Contexto;
using NDD.Api.Mapeamento.Domain.Features.Layout.Requisicao;
using NDD.Space.Base.Domain.Resposta;

namespace NDD.Api.Mapeamento.Domain.Features.Layout.Resposta
{
    public class MontarRespostaLayoutImpl : IMontarRespostaLayout<LayoutContexto, LayoutRequisicao, LayoutResposta, RespostaDeRequisicao>
    {
        public LayoutResposta ProcessarSucesso(LayoutContexto contexto)
        {
            return new LayoutResposta()
            {
                Resposta = CodigosDeRetornoBase.Info100.CriarResposta(),
                Contexto = contexto
            };
        }

        public LayoutResposta ProcessarErro(LayoutContexto contexto, RespostaDeRequisicao falha)
        {
            return new LayoutResposta()
            {
                Resposta = falha,
                Contexto = contexto
            };
        }
    }
}