using AutoMapper;
using MediatR;
using NDD.MicroServico.Base.Logs;
using NDD.Api.Mapeamento.Application.Compartilhados;
using NDD.Api.Mapeamento.Application.Dto.Layout;
using NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.Execucao;
using NDD.Api.Mapeamento.Domain.Features.Layout.Requisicao;
using NDD.Api.Mapeamento.Domain.Features.Layout.Resposta;
using NDD.Space.Base.Domain;
using NDD.Space.Base.Domain.Exceptions;

namespace NDD.Api.Mapeamento.Application.Features.Layout
{
    public class LayoutHandler : IRequestHandler<LayoutCommand, Result<Exception, LayoutDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogComIdentificadorUnico _logger;
        private readonly IExecucaoLayout<LayoutRequisicao, LayoutResposta> _layout;

        public LayoutHandler(ILogComIdentificadorUnico logger,
                              IMapper mapper,
                              IExecucaoLayout<LayoutRequisicao, LayoutResposta> layout)
        {
            _logger = logger;
            _layout = layout;
            _mapper = mapper;
        }

        public async Task<Result<Exception, LayoutDto>> Handle(LayoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var executor = new ExecucaoHandlerPadrao<
                                   IExecucaoLayout<LayoutRequisicao, LayoutResposta>,
                                   LayoutRequisicao,
                                   LayoutResposta,
                                   LayoutCommand>(_mapper);

                var resposta = await executor.ExecutorDaAcao(_layout)
                                             .Executar(request);

                return _mapper.Map<LayoutDto>(resposta.Success);
            }
            catch (Exception ex)
            {
                _logger?.Erro($"Ocorreu um erro nao tratado na execução da feature: {ex.Message} - {ex.StackTrace}");

                return new BusinessException(ErrorCodes.BadRequest, "Ocorreu um erro nao tratado na execução da feature");
            }
        }
    }
}
