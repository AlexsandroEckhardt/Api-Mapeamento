using NDD.Api.Mapeamento.Domain.Features.Layout.Requisicao;

using System;

namespace NDD.Api.Mapeamento.Domain.Features.Layout.Contexto
{
    public class LayoutContexto : IContextoLayout<LayoutRequisicao>
    {
        public Guid IdentificadorProcesso => Guid.NewGuid();

        public string NomeDoLogger => "Layout";

        public LayoutRequisicao Requisicao { get; set; }
    }
}