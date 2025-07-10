using AutoMapper;

using NDD.MicroServico.Base.Logs;
using NDD.MicroServico.Base.Negocio;
using NDD.Space.Base.Domain;
using NDD.Space.Base.Domain.Exceptions;

namespace NDD.Api.Mapeamento.Application.Compartilhados
{
    public class ExecucaoHandlerPadrao<TIProcessamento, TRequest, TResponse, TCommand>
        where TIProcessamento : IRequisicaoRNAComResposta<TRequest, TResponse>
    {
        private TIProcessamento _executorDaAcao;
        private ILogComIdentificadorUnico _logger;
        private Func<IGestorLog, INDDLog> _funcaoCriacaoNDDLog;
        private Func<TCommand, string> _funcaoRespostaNula;
        private Func<TCommand, TRequest, TRequest> _funcaoMapeamentoCustomizado;
        private readonly IMapper _mapper;

        public ExecucaoHandlerPadrao(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ExecucaoHandlerPadrao<TIProcessamento, TRequest, TResponse, TCommand> ExecutorDaAcao(TIProcessamento executorDaAcao)
        {
            _executorDaAcao = executorDaAcao;
            return this;
        }

        public ExecucaoHandlerPadrao<TIProcessamento, TRequest, TResponse, TCommand> Logger(ILogComIdentificadorUnico logger)
        {
            _logger = logger;
            return this;
        }

        public ExecucaoHandlerPadrao<TIProcessamento, TRequest, TResponse, TCommand> FuncaoCriacaoNDDLog(Func<IGestorLog, INDDLog> funcaoCriacaoNDDLog)
        {
            _funcaoCriacaoNDDLog = funcaoCriacaoNDDLog;
            return this;
        }

        public ExecucaoHandlerPadrao<TIProcessamento, TRequest, TResponse, TCommand>
            FuncaoMapeamentoCustomizado(Func<TCommand, TRequest, TRequest> funcaoMapeamentoCustomizado)
        {
            _funcaoMapeamentoCustomizado = funcaoMapeamentoCustomizado;
            return this;
        }

        public ExecucaoHandlerPadrao<TIProcessamento, TRequest, TResponse, TCommand> FuncaoMensagemRespostaNula(Func<TCommand, string> funcaoRespostaNula)
        {
            _funcaoRespostaNula = funcaoRespostaNula;
            return this;
        }

        public async Task<Result<Exception, TResponse>> Executar(TCommand request)
        {
            _logger?.CriarLog(gestor => _funcaoCriacaoNDDLog(gestor));
            try
            {
                try
                {
                    return await ExecutarFluxoProcessamento(request);
                }
                catch (Exception excecao)
                {
                    _logger?.Depurar("Erro de execucao encontrado: {0}", excecao.Message);
                    if (NaoEUmaExcecaoDeNegocio(excecao))
                        return new BusinessException(ErrorCodes.Unhandled, $"Excecao não tratada. {excecao.Message}");
                    return excecao;
                }
            }
            finally
            {
                _logger?.LiberarLog();
            }
        }

        private async Task<Result<Exception, TResponse>> ExecutarFluxoProcessamento(TCommand request)
        {
            _logger?.Depurar("Antes de adaptar mensagem de negocio");
            var requisicao = _mapper.Map<TCommand, TRequest>(request);
            if (ExisteMapeamentoCustomizado())
            {
                _logger?.Depurar("Executando mapeamento customizado");
                requisicao = _funcaoMapeamentoCustomizado(request, requisicao);
            }
            _logger?.Depurar("Executando o adaptador");
            TResponse resposta = _executorDaAcao.Processar(requisicao);
            if (RespostaNula(resposta))
            {
                string mensagem = null;
                if (FuncaoMontaRespostaExistir())
                {
                    _logger?.Depurar("Executando funcao que retorna mensagem a resposta nula");
                    mensagem = _funcaoRespostaNula(request);
                }
                else
                    mensagem = $"Resposta ao processamento da ação: {typeof(TIProcessamento)} foi 'null' e nao existe tratativa para esta situação";
                _logger?.Depurar(mensagem);
                return new BusinessException(ErrorCodes.NotFound, mensagem);
            }
            return await Task.FromResult<TResponse>(resposta);
        }

        private bool ExisteMapeamentoCustomizado()
        {
            return _funcaoMapeamentoCustomizado != null;
        }

        private bool FuncaoMontaRespostaExistir()
        {
            return _funcaoRespostaNula != null;
        }

        private bool RespostaNula(TResponse resposta)
        {
            return resposta == null;
        }

        private bool NaoEUmaExcecaoDeNegocio(Exception excecao)
        {
            return excecao is BusinessException == false;
        }

    }
}
