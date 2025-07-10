using AutoMapper;

using MediatR;

using NDD.MicroServico.Base.Logs;
using NDD.Api.Mapeamento.Application.Compartilhados;
using NDD.Api.Mapeamento.Application.Dto.Feature;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Modulos.Execucao;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Requisicao;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Resposta;
using NDD.Space.Base.Domain;
using NDD.Space.Base.Domain.Exceptions;

namespace NDD.Api.Mapeamento.Application.Features.NomeFeature
{
    public class FeatureHandler : IRequestHandler<FeatureCommand, Result<Exception, FeatureDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogComIdentificadorUnico _logger;
        private readonly IExecucaoNomeFeature<NomeFeatureRequisicao, NomeFeatureResposta> _nomeFeature;

        public FeatureHandler(ILogComIdentificadorUnico logger,
                              IMapper mapper,
                              IExecucaoNomeFeature<NomeFeatureRequisicao, NomeFeatureResposta> nomeFeature)
        {
            _logger = logger;
            _nomeFeature = nomeFeature;
            _mapper = mapper;
        }

        public async Task<Result<Exception, FeatureDto>> Handle(FeatureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var executor = new ExecucaoHandlerPadrao<
                                   IExecucaoNomeFeature<NomeFeatureRequisicao, NomeFeatureResposta>,
                                   NomeFeatureRequisicao,
                                   NomeFeatureResposta,
                                   FeatureCommand>(_mapper);

                var resposta = await executor.ExecutorDaAcao(_nomeFeature)
                                             .Executar(request);

                return _mapper.Map<FeatureDto>(resposta.Success);
            }
            catch (Exception ex)
            {
                _logger?.Erro($"Ocorreu um erro nao tratado na execução da feature: {ex.Message} - {ex.StackTrace}");

                return new BusinessException(ErrorCodes.BadRequest, "Ocorreu um erro nao tratado na execução da feature");
            }
        }
    }
}
