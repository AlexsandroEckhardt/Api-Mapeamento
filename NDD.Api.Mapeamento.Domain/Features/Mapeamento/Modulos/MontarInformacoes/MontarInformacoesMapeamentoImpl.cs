using NDD.MicroServico.Base.Logs;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Contexto;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Requisicao;
using NDD.Space.Base.Domain;
using NDD.Space.Base.Domain.Resposta;

namespace NDD.Api.Mapeamento.Domain.Features.Mapeamento.Modulos.MontarInformacoes
{
    /// <summary>
    /// Classe responsável por montar as informações que serão salvas no banco.
    /// Poderão ser utilizadas para enviar via contrato para outro endopoint.
    /// </summary>
    public class MontarInformacoesMapeamentoImpl : IMontarInformacoesMapeamento<MapeamentoMapeamento, MapeamentoRequisicao, RespostaDeRequisicao>
    {
        private readonly ILogComIdentificadorUnico _log;

        public MontarInformacoesMapeamentoImpl(ILogComIdentificadorUnico log)
        {
            _log = log;
        }

        public Result<RespostaDeRequisicao, MapeamentoMapeamento> Processar(MapeamentoMapeamento contexto)
        {
            try
            {
                _log.Depurar($"Iniciando validacao de negocio");

                return ValidacaoNegocioUm(contexto)
                       .Bind(ValidacaoNegocioDois)
                       .Match<Result<RespostaDeRequisicao, MapeamentoMapeamento>>
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

        private Result<RespostaDeRequisicao, MapeamentoMapeamento> ValidacaoNegocioUm(MapeamentoMapeamento contexto)
        {
            return contexto;
        }

        private Result<RespostaDeRequisicao, MapeamentoMapeamento> ValidacaoNegocioDois(MapeamentoMapeamento contexto)
        {
            return contexto;
        }
    }
}
