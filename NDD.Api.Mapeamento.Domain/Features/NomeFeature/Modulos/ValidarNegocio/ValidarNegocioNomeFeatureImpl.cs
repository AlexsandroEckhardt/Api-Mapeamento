using NDD.MicroServico.Base.Logs;
using NDD.Space.Base.Domain;
using NDD.Space.Base.Domain.Resposta;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Contexto;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Requisicao;

namespace NDD.Api.Mapeamento.Domain.Features.NomeFeature.Modulos.ValidarNegocio
{
    /// <summary>
    /// Classe responsável por verificar as regras de negócio do produto.
    /// </summary>
    public class ValidarNegocioNomeFeatureImpl : IValidarNegocioNomeFeature<NomeFeatureContexto, NomeFeatureRequisicao, RespostaDeRequisicao>
    {
        private readonly ILogComIdentificadorUnico _log;

        public ValidarNegocioNomeFeatureImpl(ILogComIdentificadorUnico log)
        {
            _log = log;
        }

        public Result<RespostaDeRequisicao, NomeFeatureContexto> Processar(NomeFeatureContexto contexto)
        {
            try
            {
                _log.Depurar($"Iniciando validacao de negocio");

                return ValidaNegocioUm(contexto)
                       .Bind(ValidaNegocioDois)
                       .Match<Result<RespostaDeRequisicao, NomeFeatureContexto>>
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

        private Result<RespostaDeRequisicao, NomeFeatureContexto> ValidaNegocioUm(NomeFeatureContexto contexto)
        {
            return contexto;
        }

        private Result<RespostaDeRequisicao, NomeFeatureContexto> ValidaNegocioDois(NomeFeatureContexto contexto)
        {
            return contexto;
        }

    }
}