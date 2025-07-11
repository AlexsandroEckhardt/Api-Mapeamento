using NDD.MicroServico.Base.Logs;
using NDD.Api.Mapeamento.Domain.Features.Layout.Contexto;
using NDD.Api.Mapeamento.Domain.Features.Layout.Requisicao;
using NDD.Space.Base.Domain;
using NDD.Space.Base.Domain.Resposta;

namespace NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.MontarInformacoes
{
    /// <summary>
    /// Classe responsável por montar as informações que serão salvas no banco.
    /// Poderão ser utilizadas para enviar via contrato para outro endopoint.
    /// </summary>
    public class MontarInformacoesLayoutImpl : IMontarInformacoesLayout<LayoutContexto, LayoutRequisicao, RespostaDeRequisicao>
    {
        private readonly ILogComIdentificadorUnico _log;

        public MontarInformacoesLayoutImpl(ILogComIdentificadorUnico log)
        {
            _log = log;
        }

        public Result<RespostaDeRequisicao, LayoutContexto> Processar(LayoutContexto contexto)
        {
            try
            {
                _log.Depurar($"Iniciando validacao de negocio");

                return ValidacaoNegocioUm(contexto)
                       .Bind(ValidacaoNegocioDois)
                       .Match<Result<RespostaDeRequisicao, LayoutContexto>>
                       (
                          failure: falha =>
                          {
                              _log.Depurar($"Validacao de negocio finalizada com erro");

                              return falha;
                          },
                          success: sucesso =>
                          {
                              _log.Depurar($"Validacao de negocio finalizada com sucesso");

                              return contexto;
                          }
                       );
            }
            finally
            {
                _log?.LiberarLog();
            }
        }

        private Result<RespostaDeRequisicao, LayoutContexto> ValidacaoNegocioUm(LayoutContexto contexto)
        {
            return contexto;
        }

        private Result<RespostaDeRequisicao, LayoutContexto> ValidacaoNegocioDois(LayoutContexto contexto)
        {
            return contexto;
        }
    }
}
