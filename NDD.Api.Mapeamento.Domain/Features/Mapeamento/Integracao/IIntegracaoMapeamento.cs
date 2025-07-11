using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Resposta;


namespace NDD.Api.Mapeamento.Domain.Features.Mapeamento.Integracao
{
    public interface IIntegracaoMapeamento
    {
        Task ExecutarAsync(MapeamentoResposta resposta, object contexto = null);
    }
}
