using NDD.MicroServico.Base.Logs;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Contexto;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Requisicao;
using NDD.Space.Base.Domain;
using NDD.Space.Base.Domain.Resposta;

namespace NDD.Api.Mapeamento.Domain.Features.NomeFeature.Modulos.MontarInformacoes
{
    /// <summary>
    /// Classe responsável por montar as informações que serão salvas no banco.
    /// Poderão ser utilizadas para enviar via contrato para outro endopoint.
    /// </summary>
    public class MontarInformacoesNomeFeatureImpl : IMontarInformacoesNomeFeature<NomeFeatureContexto, NomeFeatureRequisicao, RespostaDeRequisicao>
    {
        private readonly ILogComIdentificadorUnico _log;

        public MontarInformacoesNomeFeatureImpl(ILogComIdentificadorUnico log)
        {
            _log = log;
        }

        public Result<RespostaDeRequisicao, NomeFeatureContexto> Processar(NomeFeatureContexto contexto)
        {
            try
            {
                _log.Depurar($"Iniciando validacao de negocio");

                return ValidacaoNegocioUm(contexto)
                       .Bind(ValidacaoNegocioDois)
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

        private Result<RespostaDeRequisicao, NomeFeatureContexto> ValidacaoNegocioUm(NomeFeatureContexto contexto)
        {
            return contexto;
        }

        private Result<RespostaDeRequisicao, NomeFeatureContexto> ValidacaoNegocioDois(NomeFeatureContexto contexto)
        {
            return contexto;
        }
    }
}
