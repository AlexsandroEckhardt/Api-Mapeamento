using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Requisicao;

using System;

namespace NDD.Api.Mapeamento.Domain.Features.Mapeamento.Contexto
{
    public class MapeamentoMapeamento : IContextoMapeamento<MapeamentoRequisicao>
    {
        public Guid IdentificadorProcesso => Guid.NewGuid();

        public string NomeDoLogger => "Mapeamento";

        public MapeamentoRequisicao Requisicao { get; set; }
    }
}