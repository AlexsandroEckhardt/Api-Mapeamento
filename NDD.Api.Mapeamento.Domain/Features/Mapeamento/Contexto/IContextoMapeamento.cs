using System;

namespace NDD.Api.Mapeamento.Domain.Features.Mapeamento.Contexto
{
    public interface IContextoMapeamento<TRequisicao>
    {
        TRequisicao Requisicao { get; set; }

        string NomeDoLogger { get; }
    }
}
