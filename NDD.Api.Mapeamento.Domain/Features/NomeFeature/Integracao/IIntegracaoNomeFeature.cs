using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Resposta;

using System.Threading.Tasks;

namespace NDD.Api.Mapeamento.Domain.Features.NomeFeature.Integracao
{
    public interface IIntegracaoNomeFeature
    {
        Task ExecutarAsync(NomeFeatureResposta resposta, object contexto = null);
    }
}
