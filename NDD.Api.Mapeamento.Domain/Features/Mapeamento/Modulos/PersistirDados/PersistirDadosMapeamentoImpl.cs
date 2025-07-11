using NDD.MicroServico.Base.Logs;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Contexto;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Requisicao;
using NDD.Space.Base.Domain;
using NDD.Space.Base.Domain.Resposta;

using System;

namespace NDD.Api.Mapeamento.Domain.Features.Mapeamento.Modulos.PersistirDados
{
    /// <summary>
    /// Classe responsável por acessar o repositório e efetuar as transasões no banco.
    /// </summary>
    public class PersistirDadosMapeamentoImpl : IPersistirDadosMapeamento<MapeamentoMapeamento, MapeamentoRequisicao, RespostaDeRequisicao>
    {
        private readonly ILogComIdentificadorUnico _log;

        public PersistirDadosMapeamentoImpl(ILogComIdentificadorUnico log)
        {
            _log = log;
        }

        public Result<RespostaDeRequisicao, MapeamentoMapeamento> Processar(MapeamentoMapeamento contexto)
        {
            try
            {
                _log.Depurar($"Iniciando persistencia");

                PersistirDados(contexto)
                .Match<Result<RespostaDeRequisicao, MapeamentoMapeamento>>
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

        private Result<RespostaDeRequisicao, MapeamentoMapeamento> PersistirDados(MapeamentoMapeamento contexto)
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
