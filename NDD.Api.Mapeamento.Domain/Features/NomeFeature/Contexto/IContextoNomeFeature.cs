using System;

namespace NDD.Api.Mapeamento.Domain.Features.NomeFeature.Contexto
{
    public interface IContextoNomeFeature<TRequisicao>
    {
        TRequisicao Requisicao { get; set; }

        string NomeDoLogger { get; }
    }
}
