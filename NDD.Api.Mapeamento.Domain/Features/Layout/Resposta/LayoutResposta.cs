using NDD.Api.Mapeamento.Domain.Features.Layout.Contexto;
using NDD.Space.Base.Domain.Resposta;

namespace NDD.Api.Mapeamento.Domain.Features.Layout.Resposta
{
    public class LayoutResposta
    {
        public RespostaDeRequisicao Resposta { get; set; }

        public LayoutContexto Contexto { get; set; }
    }
}