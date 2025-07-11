using NDD.MicroServico.Base.Logs;
using NDD.Space.Base.Domain;
using NDD.Space.Base.Domain.Resposta;
using NDD.Api.Mapeamento.Domain.Features.Layout.Contexto;
using NDD.Api.Mapeamento.Domain.Features.Layout.Requisicao;

namespace NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.ValidarNegocio
{
    /// <summary>
    /// Classe responsável por verificar as regras de negócio do produto.
    /// </summary>
    public class ValidarNegocioLayoutImpl : IValidarNegocioLayout<LayoutContexto, LayoutRequisicao, RespostaDeRequisicao>
    {
        private readonly ILogComIdentificadorUnico _log;

        public ValidarNegocioLayoutImpl(ILogComIdentificadorUnico log)
        {
            _log = log;
        }

        public Result<RespostaDeRequisicao, LayoutContexto> Processar(LayoutContexto contexto)
        {
            try
            {
                _log.Depurar($"Iniciando validacao de negocio");

                return ValidaNegocioUm(contexto)
                       .Bind(ValidaNegocioDois)
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

        private Result<RespostaDeRequisicao, LayoutContexto> ValidaNegocioUm(LayoutContexto contexto)
        {
            return contexto;
        }

        private Result<RespostaDeRequisicao, LayoutContexto> ValidaNegocioDois(LayoutContexto contexto)
        {
            return contexto;
        }

    }
}