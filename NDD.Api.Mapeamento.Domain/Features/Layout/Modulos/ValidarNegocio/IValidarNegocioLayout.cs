﻿using NDD.Api.Mapeamento.Domain.Features.Layout.Contexto;
using NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.Execucao;
using NDD.Space.Base.Domain;

namespace NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.ValidarNegocio
{
    public interface IValidarNegocioLayout<TContexto, TRequisicao, TFalha> : IExecucaoLayout<TContexto, Result<TFalha, TContexto>>
                                                                                  where TContexto : IContextoLayout<TRequisicao>
    {

    }
}