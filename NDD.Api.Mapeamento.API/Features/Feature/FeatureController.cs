using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

using NDD.MicroServico.Base.Logs;
using NDD.Api.Mapeamento.API.Base;
using NDD.Api.Mapeamento.API.Exceptions;
using NDD.Api.Mapeamento.Application.Dto.Feature;
using NDD.Api.Mapeamento.Application.Features.NomeFeature;

namespace NDD.Api.Mapeamento.API.Features.Feature
{
    [Route("api/[controller]")]
    public class LayoutsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public LayoutsController(IMediator mediator,
                                 ILogComIdentificadorUnico logger,
                                 IMapper mapper)
                                 : base(logger, mapper)
        {
            _mediator = mediator;
        }

        #region HttpGet

        /// <summary>
        /// Listar todos os layouts disponiveis {Sem parametro}
        /// </summary>
        /// <param name="featureCommand">Contém as informações necessarias do command</param>
        /// <response code="200">Success, Chamada realizada com sucesso.</response>
        /// <response code="400">Bad Request, chamada inválida.</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(FeatureViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionPayload), StatusCodes.Status400BadRequest)]
        [HttpGet("ListarTodosLayouts")]
        [EnableRateLimiting("PolicyFeatureListarLayouts")]
        public async Task<IActionResult> FeatureGetListarTodosLayouts([FromBody] FeatureCommand featureCommand)
        {
            if (featureCommand != null)
                featureCommand.IdentificadorCliente = ObterIdentificadorCliente();

            return await Executar<FeatureCommand, FeatureDto, FeatureViewModel>(
                                  featureCommand,
                                  () => "A requisição não pode ser nula",
                                  () => _mediator.Send(featureCommand));
        }


        /// <summary>
        /// Listar layouts destino {Layout Origem}
        /// </summary>
        /// <param name="featureCommand">Contém as informações necessarias do command</param>
        /// <response code="200">Success, Chamada realizada com sucesso.</response>
        /// <response code="400">Bad Request, chamada inválida.</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(FeatureViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionPayload), StatusCodes.Status400BadRequest)]
        [HttpGet("ListarLayoutsDestino")]
        [EnableRateLimiting("PolicyFeatureListarLayoutsDestino")]
        public async Task<IActionResult> FeatureGetListarLayoutsDestino([FromBody] FeatureCommand featureCommand)
        {
            if (featureCommand != null)
                featureCommand.IdentificadorCliente = ObterIdentificadorCliente();

            return await Executar<FeatureCommand, FeatureDto, FeatureViewModel>(
                                  featureCommand,
                                  () => "A requisição não pode ser nula",
                                  () => _mediator.Send(featureCommand));
        }

        #endregion

    }

    [Route("api/[controller]")]
    public class MapeamentoController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public MapeamentoController(IMediator mediator,
                                 ILogComIdentificadorUnico logger,
                                 IMapper mapper)
                                 : base(logger, mapper)
        {
            _mediator = mediator;
        }

        #region HttpPost

        /// <summary>
        /// Realiza o mapeamento nos layouts selecionados {Layout Origem, Layout Destino, Documento}
        /// </summary>
        /// <param name="featureCommand">Contém as informações necessarias do command</param>
        /// <response code="200">Success, Chamada realizada com sucesso.</response>
        /// <response code="400">Bad Request, chamada inválida.</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(FeatureViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionPayload), StatusCodes.Status400BadRequest)]
        [HttpPost("ExecutarMapeamento")]
        [EnableRateLimiting("PolicyFeatureExecutarMapeamento")]
        public async Task<IActionResult> FeaturePostExecutarMapeamento([FromBody] FeatureCommand featureCommand)
        {
            if (featureCommand != null)
                featureCommand.IdentificadorCliente = ObterIdentificadorCliente();

            return await Executar<FeatureCommand, FeatureDto, FeatureViewModel>(
                                  featureCommand,
                                  () => "A requisição não pode ser nula",
                                  () => _mediator.Send(featureCommand));
        }
        #endregion
    }
}

