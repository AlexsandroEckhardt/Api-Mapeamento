using NDD.MicroServico.Base.Logs;
using NDD.MicroServico.Base.ProvedorServicos;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Contexto;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Modulos.MontarInformacoes;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Modulos.PersistirDados;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Modulos.ValidarNegocio;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Resposta;
using NDD.Space.Base.Domain;

using System.Diagnostics;

namespace NDD.Api.Mapeamento.Domain.Features.Mapeamento.Modulos.Execucao
{
    /// <summary>
    /// Classe principal onde faz o gerenciamento de todos os passos da execução da feature
    /// </summary>
    public class ExecucaoMapeamentoImpl<TContexto, TRequisicao, TResposta, TFalha> : IExecucaoMapeamento<TRequisicao, TResposta> where TContexto : IContextoMapeamento<TRequisicao>
    {
        private readonly IValidarNegocioMapeamento<TContexto, TRequisicao, TFalha> _validarNegocio;
        private readonly IMontarInformacoesMapeamento<TContexto, TRequisicao, TFalha> _montarInformacoes;
        private readonly IPersistirDadosMapeamento<TContexto, TRequisicao, TFalha> _persistirDados;
        private readonly ILogComIdentificadorUnico _log;

        private readonly IMontarRespostaMapeamento<TContexto, TRequisicao, TResposta, TFalha> _montarResposta;

        private Stopwatch _tempoExecucao;

        public ExecucaoMapeamentoImpl(IProvedorServico provedor, ILogComIdentificadorUnico log)
        {
            _validarNegocio = provedor.RetornarServico<IValidarNegocioMapeamento<TContexto, TRequisicao, TFalha>>();
            _montarInformacoes = provedor.RetornarServico<IMontarInformacoesMapeamento<TContexto, TRequisicao, TFalha>>();
            _persistirDados = provedor.RetornarServico<IPersistirDadosMapeamento<TContexto, TRequisicao, TFalha>>();

            _montarResposta = provedor.RetornarServico<IMontarRespostaMapeamento<TContexto, TRequisicao, TResposta, TFalha>>();

            _log = log;
        }

        public TResposta Processar(TRequisicao requisicao)
        {
            TContexto contexto = (TContexto)typeof(TContexto).Assembly.CreateInstance(typeof(TContexto).FullName);

            _tempoExecucao = Stopwatch.StartNew();

            contexto.Requisicao = requisicao;

            _log.IdentificadorLog = "IdentificadorDoLog";
            _log?.CriarLog(gestor => gestor.GetLogger($"{contexto.NomeDoLogger}"));

            try
            {
                _log?.Depurar($"Iniciando execucao da feature: {contexto.NomeDoLogger}");

                return _validarNegocio.Processar(contexto)
                        .Match
                        (
                            failure: falha => FinalizarProcesso(contexto, () => _montarResposta.ProcessarErro(contexto, falha)),
                            success: _ => FinalizarProcesso(contexto, () => _montarResposta.ProcessarSucesso(contexto))
                        );
            }
            finally
            {
                _tempoExecucao.Stop();

                _log?.Depurar($"Finalizando execucao da feature {contexto.NomeDoLogger} em {_tempoExecucao.ElapsedMilliseconds} ms");

                _log?.LiberarLog();
            }
        }

        private TResposta FinalizarProcesso(TContexto contexto, Func<TResposta> funcao)
        {
            _montarInformacoes.Processar(contexto)
              .Bind(_persistirDados.Processar);

            return funcao();
        }
    }
}