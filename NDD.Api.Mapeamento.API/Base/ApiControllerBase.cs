using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using NDD.MicroServico.Base.Extensoes;
using NDD.MicroServico.Base.Logs;
using NDD.Api.Mapeamento.API.Exceptions;
using NDD.Space.Base.Domain;
using NDD.Space.Base.Domain.Exceptions;

using System.Net;
using System.Security.Claims;

namespace NDD.Api.Mapeamento.API.Base
{
    //[Authorize]
    public class ApiControllerBase : ControllerBase
    {
        private readonly ILogComIdentificadorUnico _logger;
        private readonly IMapper _mapper;

        public ApiControllerBase(ILogComIdentificadorUnico logger,
                                 IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        #region Executar

        protected async Task<IActionResult> Executar<TCommand, TResponse, TResponseView>(
                                                     TCommand? requisicao,
                                                     Func<string> funcaoErro,
                                                     Func<Task<Result<Exception, TResponse>>> executarAcaoMediador)
        {
            _logger?.CriarLog(gestor => gestor?.GetLogger(typeof(TCommand).Name));

            IActionResult resultadoDaAcaoControlador;

            try
            {
                _logger?.Depurar("Inicio da ação: {0}", requisicao.ReportarTodasPropriedades());

                if (RequisicaoInvalida(requisicao))
                {
                    string? errosEspecificos = CapturarErrosEspecificos();

                    var businessException = new BusinessException(ErrorCodes.InvalidObject, funcaoErro() + " Erros: " + errosEspecificos);

                    _logger?.Depurar("Erro detectado: {0}-{1}", businessException.ErrorCode, businessException.Message);

                    resultadoDaAcaoControlador = HandleFailure(businessException);
                }
                else
                {
                    _logger?.Depurar("Executando o mediador");

                    var resultado = await executarAcaoMediador();

                    resultadoDaAcaoControlador = HandleQuery<TResponse, TResponseView>(resultado);
                }

                return resultadoDaAcaoControlador;
            }
            finally
            {
                _logger?.Depurar("Termino da ação");
                _logger?.LiberarLog();
            }
        }

        private string? CapturarErrosEspecificos()
        {
            if (!ModelState.Values.Any())
                return "Não foi possivel fornecer mensagem mais detalhada";

            var listaErros = ModelState.Values.Select(campo =>
            {
                return campo.Errors
                            .Select(erro => string.IsNullOrEmpty(erro.ErrorMessage) ? erro.Exception?.Message : erro.ErrorMessage)
                            .Aggregate(string.Empty, (erroAtual, erroProximo) => $"{erroAtual}[Mensagem: {erroProximo}]");
            });

            return listaErros.Any() ? listaErros.FirstOrDefault() :
                                      listaErros.Aggregate(string.Empty, (atual, proximo) => CapturarListaErrosAgregados(atual, proximo));
        }

        private static string CapturarListaErrosAgregados(string atual, string proximo)
        {
            return string.IsNullOrEmpty(atual) ? proximo : atual + ", " + proximo;
        }

        private static bool RequisicaoInvalida(object? requisicao)
        {
            return requisicao == null;
        }

        protected string? ObterIdentificadorCliente()
        {
            var identityClain = Request.HttpContext.User.Identity as ClaimsIdentity;

            var clientIdentifier = identityClain?.FindFirst("client_id")?.Value;

            return clientIdentifier;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Manuseia o result. Verifica se a resposta é uma falha ou sucesso, retornando os dados apropriados.
        /// É importante destacar que este método realiza o mapeamento da classe TSource em TResult
        /// </summary>
        /// <typeparam name="TSource">Classe de origem (ex.: domínio)</typeparam>
        /// <typeparam name="TResult">ViewModel</typeparam>
        /// <param name="result">Objeto Result retornado pelas chamadas.</param>
        /// <returns>Resposta apropriada baseado no result enviado como parâmetro</returns>
        protected IActionResult HandleQuery<TSource, TResult>(Result<Exception, TSource> result)
        {
            return result.IsSuccess ? this.Ok(_mapper.Map<TSource, TResult>(result.Success)) : this.HandleFailure(result.Failure);
        }

        /// <summary>
        /// Verifica a exceção passada por parametro para passar o StatusCode correto para o frontend.
        /// </summary>
        /// <typeparam name="T">Qualquer classe que herde de Exeption</typeparam>
        /// <param name="exceptionToHandle">obj de exceção</param>
        /// <returns></returns>
        protected IActionResult HandleFailure<T>(T exceptionToHandle) where T : Exception
        {
            HttpStatusCode code;
            ExceptionPayload payload;

            if (exceptionToHandle is BusinessException)
            {
                code = HttpStatusCode.BadRequest;
                payload = ExceptionPayload.New(
                    exceptionToHandle,
                    (exceptionToHandle as BusinessException).ErrorCode.GetHashCode());
            }
            else if (exceptionToHandle is FluentValidation.ValidationException)
            {
                code = HttpStatusCode.BadRequest;
                payload = ExceptionPayload.New(
                                exceptionToHandle,
                                ErrorCodes.BadRequest.GetHashCode(),
                                new ValidationFailureMapper().Map((exceptionToHandle as FluentValidation.ValidationException).Errors));
            }
            else
            {
                code = HttpStatusCode.InternalServerError;
                payload = ExceptionPayload.New(
                    new Exception("Ação não pode ser realizada"),
                    ErrorCodes.Unhandled.GetHashCode());
            }

            return this.StatusCode(code.GetHashCode(), payload);
        }
    }

    #endregion Handlers
}