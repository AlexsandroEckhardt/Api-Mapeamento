using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Requisicao;

using System;

namespace NDD.Api.Mapeamento.Domain.Features.NomeFeature.Contexto
{
    public class NomeFeatureContexto : IContextoNomeFeature<NomeFeatureRequisicao>
    {
        public Guid IdentificadorProcesso => Guid.NewGuid();

        public string NomeDoLogger => "NomeFeature";

        public NomeFeatureRequisicao Requisicao { get; set; }
    }
}