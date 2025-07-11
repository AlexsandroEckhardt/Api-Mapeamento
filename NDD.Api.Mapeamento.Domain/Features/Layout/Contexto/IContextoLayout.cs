using System;

namespace NDD.Api.Mapeamento.Domain.Features.Layout.Contexto
{
    public interface IContextoLayout<TRequisicao>
    {
        TRequisicao Requisicao { get; set; }

        string NomeDoLogger { get; }
    }
}
