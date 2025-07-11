using NDD.Api.Mapeamento.Domain.Features.Layout.Resposta;

namespace NDD.Api.Mapeamento.Domain.Features.Layout.Integracao
{
    public interface IIntegracaoLayout
    {
        Task ExecutarAsync(LayoutResposta resposta, object contexto = null);
    }
}
