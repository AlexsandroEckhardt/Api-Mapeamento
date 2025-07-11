using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Contexto;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Modulos.Execucao;
using NDD.Space.Base.Domain;

namespace NDD.Api.Mapeamento.Domain.Features.Mapeamento.Modulos.MontarInformacoes
{
    public interface IMontarInformacoesMapeamento<TContexto, TRequisicao, TFalha> : IExecucaoMapeamento<TContexto, Result<TFalha, TContexto>>
                                                                                     where TContexto : IContextoMapeamento<TRequisicao>
    {

    }
}