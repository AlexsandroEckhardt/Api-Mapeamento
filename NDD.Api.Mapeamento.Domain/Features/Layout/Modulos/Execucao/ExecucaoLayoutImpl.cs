using NDD.MicroServico.Base.Logs;
using NDD.MicroServico.Base.ProvedorServicos;
using NDD.Api.Mapeamento.Domain.Features.Layout.Contexto;
using NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.MontarInformacoes;
using NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.PersistirDados;
using NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.ValidarNegocio;
using NDD.Api.Mapeamento.Domain.Features.Layout.Resposta;
using NDD.Space.Base.Domain;

using System.Diagnostics;

namespace NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.Execucao
{
    /// <summary>
    /// Classe principal onde faz o gerenciamento de todos os passos da execução da feature
    /// </summary>
    public class ExecucaoLayoutImpl<TContexto, TRequisicao, TResposta, TFalha> : IExecucaoLayout<TRequisicao, TResposta> where TContexto : IContextoLayout<TRequisicao>
    {
        private readonly IValidarNegocioLayout<TContexto, TRequisicao, TFalha> _validarNegocio;
        private readonly IMontarInformacoesLayout<TContexto, TRequisicao, TFalha> _montarInformacoes;
        private readonly IPersistirDadosLayout<TContexto, TRequisicao, TFalha> _persistirDados;
        private readonly ILogComIdentificadorUnico _log;

        private readonly IMontarRespostaLayout<TContexto, TRequisicao, TResposta, TFalha> _montarResposta;

        private Stopwatch _tempoExecucao;

        public ExecucaoLayoutImpl(IProvedorServico provedor, ILogComIdentificadorUnico log)
        {
            _validarNegocio = provedor.RetornarServico<IValidarNegocioLayout<TContexto, TRequisicao, TFalha>>();
            _montarInformacoes = provedor.RetornarServico<IMontarInformacoesLayout<TContexto, TRequisicao, TFalha>>();
            _persistirDados = provedor.RetornarServico<IPersistirDadosLayout<TContexto, TRequisicao, TFalha>>();

            _montarResposta = provedor.RetornarServico<IMontarRespostaLayout<TContexto, TRequisicao, TResposta, TFalha>>();

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