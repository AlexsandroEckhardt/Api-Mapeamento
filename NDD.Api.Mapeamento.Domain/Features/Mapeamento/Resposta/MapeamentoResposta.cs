using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Contexto;
using NDD.Space.Base.Domain.Resposta;

namespace NDD.Api.Mapeamento.Domain.Features.Mapeamento.Resposta
{
    public class MapeamentoResposta
    {
        public RespostaDeRequisicao Resposta { get; set; }

        public MapeamentoMapeamento Contexto { get; set; }
    }
}