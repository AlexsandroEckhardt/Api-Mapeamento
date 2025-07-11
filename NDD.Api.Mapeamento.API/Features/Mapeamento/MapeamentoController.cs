using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NDD.MicroServico.Base.Logs;
using NDD.Api.Mapeamento.API.Base;
using NDD.Api.Mapeamento.API.Exceptions;
using NDD.Api.Mapeamento.Application.Dto.Mapeamento;
using NDD.Api.Mapeamento.Application.Features.Mapeamento;

namespace NDD.Api.Mapeamento.API.Features.Mapeamento
{
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

        #region HttpGet

        /// <summary>
        /// Executar mapeamento
        /// </summary>
        /// <param name="mapeamentoCommand">Contém as informações necessarias do command</param>
        /// <response code="200">Success, Chamada realizada com sucesso.</response>
        /// <response code="400">Bad Request, chamada inválida.</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(MapeamentoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionPayload), StatusCodes.Status400BadRequest)]
        [HttpPost("ExecutarMapeamento")]
        [EnableRateLimiting("PolicyExecutarMapeamento")]
        public async Task<IActionResult> GetListarTodosMapeamentos([FromBody] MapeamentoCommand MapeamentoCommand)
        {
            if (MapeamentoCommand != null)
                MapeamentoCommand.IdentificadorCliente = ObterIdentificadorCliente();

            return await Executar<MapeamentoCommand, MapeamentoDto, MapeamentoViewModel>(
                                  MapeamentoCommand,
                                  () => "A requisição não pode ser nula",
                                  () => _mediator.Send(MapeamentoCommand));
        }

        #endregion

    }
}

