using NDD.Space.Base.Domain;

using System.Diagnostics.CodeAnalysis;

namespace NDD.Api.Mapeamento.Domain.Features.NomeFeature.Entidade
{
    [ExcludeFromCodeCoverage]
    public class NomeFeatureVo : Entity
    {
        public string? Cnpj { get; set; }
    }
}
