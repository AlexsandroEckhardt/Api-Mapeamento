using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NDD.MicroServico.Base.Logs;
using NDD.Api.Mapeamento.API.Base;
using NDD.Api.Mapeamento.API.Exceptions;
using NDD.Api.Mapeamento.Application.Dto.Layout;
using NDD.Api.Mapeamento.Application.Features.Layout;

namespace NDD.Api.Mapeamento.API.Features.Layout
{
    [Route("api/[controller]")]
    public class LayoutController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public LayoutController(IMediator mediator,
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
        /// <param name="layoutCommand">Contém as informações necessarias do command</param>
        /// <response code="200">Success, Chamada realizada com sucesso.</response>
        /// <response code="400">Bad Request, chamada inválida.</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(LayoutViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionPayload), StatusCodes.Status400BadRequest)]
        [HttpGet("ListarTodosLayouts")]
        [EnableRateLimiting("PolicyListarLayouts")]
        public async Task<IActionResult> GetListarTodosLayouts([FromBody] LayoutCommand layoutCommand)
        {
            if (layoutCommand != null)
                layoutCommand.IdentificadorCliente = ObterIdentificadorCliente();

            return await Executar<LayoutCommand, LayoutDto, LayoutViewModel>(
                                  layoutCommand,
                                  () => "A requisição não pode ser nula",
                                  () => _mediator.Send(layoutCommand));
        }


        /// <summary>
        /// Listar layouts destino {Layout Origem}
        /// </summary>
        /// <param name="layoutCommand">Contém as informações necessarias do command</param>
        /// <response code="200">Success, Chamada realizada com sucesso.</response>
        /// <response code="400">Bad Request, chamada inválida.</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(LayoutViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionPayload), StatusCodes.Status400BadRequest)]
        [HttpGet("ListarLayoutsDestino")]
        [EnableRateLimiting("PolicyFeatureListarLayoutsDestino")]
        public async Task<IActionResult> FeatureGetListarLayoutsDestino([FromBody] LayoutCommand layoutCommand)
        {
            if (layoutCommand != null)
                layoutCommand.IdentificadorCliente = ObterIdentificadorCliente();

            return await Executar<LayoutCommand, LayoutDto, LayoutViewModel>(
                                  layoutCommand,
                                  () => "A requisição não pode ser nula",
                                  () => _mediator.Send(layoutCommand));
        }

        #endregion

    }
}

