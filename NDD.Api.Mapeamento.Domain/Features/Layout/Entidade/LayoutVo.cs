﻿using NDD.Space.Base.Domain;

using System.Diagnostics.CodeAnalysis;

namespace NDD.Api.Mapeamento.Domain.Features.Layout.Entidade
{
    [ExcludeFromCodeCoverage]
    public class MapeamentoVo : Entity
    {
        public string? Cnpj { get; set; }
    }
}
