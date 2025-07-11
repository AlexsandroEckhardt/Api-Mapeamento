using AutoMapper;
using MediatR;
using NDD.MicroServico.Base.Logs;
using NDD.Api.Mapeamento.Application.Compartilhados;
using NDD.Api.Mapeamento.Application.Dto.Mapeamento;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Modulos.Execucao;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Requisicao;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Resposta;
using NDD.Space.Base.Domain;
using NDD.Space.Base.Domain.Exceptions;

namespace NDD.Api.Mapeamento.Application.Features.Mapeamento
{
    public class MapeamentoHandler : IRequestHandler<MapeamentoCommand, Result<Exception, MapeamentoDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogComIdentificadorUnico _logger;
        private readonly IExecucaoMapeamento<MapeamentoRequisicao, MapeamentoResposta> _Mapeamento;

        public MapeamentoHandler(ILogComIdentificadorUnico logger,
                              IMapper mapper,
                              IExecucaoMapeamento<MapeamentoRequisicao, MapeamentoResposta> Mapeamento)
        {
            _logger = logger;
            _Mapeamento = Mapeamento;
            _mapper = mapper;
        }

        public async Task<Result<Exception, MapeamentoDto>> Handle(MapeamentoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var executor = new ExecucaoHandlerPadrao<
                                   IExecucaoMapeamento<MapeamentoRequisicao, MapeamentoResposta>,
                                   MapeamentoRequisicao,
                                   MapeamentoResposta,
                                   MapeamentoCommand>(_mapper);

                var resposta = await executor.ExecutorDaAcao(_Mapeamento)
                                             .Executar(request);

                return _mapper.Map<MapeamentoDto>(resposta.Success);
            }
            catch (Exception ex)
            {
                _logger?.Erro($"Ocorreu um erro nao tratado na execução da feature: {ex.Message} - {ex.StackTrace}");

                return new BusinessException(ErrorCodes.BadRequest, "Ocorreu um erro nao tratado na execução da feature");
            }
        }
    }
}
