using NDD.MicroServico.Base.Logs;
using NDD.Space.Base.Domain;
using NDD.Space.Base.Domain.Resposta;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Contexto;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Requisicao;

namespace NDD.Api.Mapeamento.Domain.Features.Mapeamento.Modulos.ValidarNegocio
{
    /// <summary>
    /// Classe responsável por verificar as regras de negócio do produto.
    /// </summary>
    public class ValidarNegocioMapeamentoImpl : IValidarNegocioMapeamento<MapeamentoMapeamento, MapeamentoRequisicao, RespostaDeRequisicao>
    {
        private readonly ILogComIdentificadorUnico _log;

        public ValidarNegocioMapeamentoImpl(ILogComIdentificadorUnico log)
        {
            _log = log;
        }

        public Result<RespostaDeRequisicao, MapeamentoMapeamento> Processar(MapeamentoMapeamento contexto)
        {
            try
            {
                _log.Depurar($"Iniciando validacao de negocio");

                return ValidaNegocioUm(contexto)
                       .Bind(ValidaNegocioDois)
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

        private Result<RespostaDeRequisicao, MapeamentoMapeamento> ValidaNegocioUm(MapeamentoMapeamento contexto)
        {
            return contexto;
        }

        private Result<RespostaDeRequisicao, MapeamentoMapeamento> ValidaNegocioDois(MapeamentoMapeamento contexto)
        {
            return contexto;
        }

    }
}