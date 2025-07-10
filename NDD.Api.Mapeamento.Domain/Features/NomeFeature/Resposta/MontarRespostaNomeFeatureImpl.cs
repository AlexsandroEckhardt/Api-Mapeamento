using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Contexto;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Requisicao;
using NDD.Space.Base.Domain.Resposta;

namespace NDD.Api.Mapeamento.Domain.Features.NomeFeature.Resposta
{
    public class MontarRespostaNomeFeatureImpl : IMontarRespostaNomeFeature<NomeFeatureContexto, NomeFeatureRequisicao, NomeFeatureResposta, RespostaDeRequisicao>
    {
        public NomeFeatureResposta ProcessarSucesso(NomeFeatureContexto contexto)
        {
            return new NomeFeatureResposta()
            {
                Resposta = CodigosDeRetornoBase.Info100.CriarResposta(),
                Contexto = contexto
            };
        }

        public NomeFeatureResposta ProcessarErro(NomeFeatureContexto contexto, RespostaDeRequisicao falha)
        {
            return new NomeFeatureResposta()
            {
                Resposta = falha,
                Contexto = contexto
            };
        }
    }
}