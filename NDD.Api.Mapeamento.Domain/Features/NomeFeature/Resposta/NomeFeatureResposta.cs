using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Contexto;
using NDD.Space.Base.Domain.Resposta;

namespace NDD.Api.Mapeamento.Domain.Features.NomeFeature.Resposta
{
    public class NomeFeatureResposta
    {
        public RespostaDeRequisicao Resposta { get; set; }

        public NomeFeatureContexto Contexto { get; set; }
    }
}