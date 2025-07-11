using NDD.MicroServico.Base.Logs;
using NDD.Api.Mapeamento.Domain.Features.Layout.Contexto;
using NDD.Api.Mapeamento.Domain.Features.Layout.Requisicao;
using NDD.Space.Base.Domain;
using NDD.Space.Base.Domain.Resposta;

using System;

namespace NDD.Api.Mapeamento.Domain.Features.Layout.Modulos.PersistirDados
{
    /// <summary>
    /// Classe responsável por acessar o repositório e efetuar as transasões no banco.
    /// </summary>
    public class PersistirDadosLayoutImpl : IPersistirDadosLayout<LayoutContexto, LayoutRequisicao, RespostaDeRequisicao>
    {
        private readonly ILogComIdentificadorUnico _log;

        public PersistirDadosLayoutImpl(ILogComIdentificadorUnico log)
        {
            _log = log;
        }

        public Result<RespostaDeRequisicao, LayoutContexto> Processar(LayoutContexto contexto)
        {
            try
            {
                _log.Depurar($"Iniciando persistencia");

                PersistirDados(contexto)
                .Match<Result<RespostaDeRequisicao, LayoutContexto>>
                (
                   failure: falha =>
                   {
                       _log.Depurar($"Persistencia finalizada com erro");

                       return falha;
                   },
                   success: sucesso =>
                   {
                       _log.Depurar($"Persistencia finalizada com sucesso");

                       return contexto;
                   }
                );

                return contexto;
            }
            finally
            {
                _log?.LiberarLog();
            }
        }

        private Result<RespostaDeRequisicao, LayoutContexto> PersistirDados(LayoutContexto contexto)
        {
            try
            {

                return contexto;
            }
            catch (Exception ex)
            {
                _log.Erro($"Ocorreu um erro na persistencia: {ex.Message} => {ex.StackTrace}");

                return CodigosDeRetornoBase.Erro999.CriarResposta(ex.Message);
            }
        }
    }
}
