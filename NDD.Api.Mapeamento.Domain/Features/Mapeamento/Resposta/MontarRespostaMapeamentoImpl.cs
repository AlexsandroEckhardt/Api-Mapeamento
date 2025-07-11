using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Contexto;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Requisicao;
using NDD.Space.Base.Domain.Resposta;

namespace NDD.Api.Mapeamento.Domain.Features.Mapeamento.Resposta
{
    public class MontarRespostaMapeamentoImpl : IMontarRespostaMapeamento<MapeamentoMapeamento, MapeamentoRequisicao, MapeamentoResposta, RespostaDeRequisicao>
    {
        public MapeamentoResposta ProcessarSucesso(MapeamentoMapeamento contexto)
        {
            return new MapeamentoResposta()
            {
                Resposta = CodigosDeRetornoBase.Info100.CriarResposta(),
                Contexto = contexto
            };
        }

        public MapeamentoResposta ProcessarErro(MapeamentoMapeamento contexto, RespostaDeRequisicao falha)
        {
            return new MapeamentoResposta()
            {
                Resposta = falha,
                Contexto = contexto
            };
        }
    }
}