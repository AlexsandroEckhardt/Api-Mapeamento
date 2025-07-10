namespace NDD.Api.Mapeamento.API.Exceptions
{
    /// <summary>
    ///  Classe que representa uma exceção lançada para o client como resposta.
    ///</summary>
    public class ExceptionPayload
    {
        public int Codigo { get; set; }

        public string? Mensagem { get; set; }

        public IEnumerable<ValidationFailure>? Erros { get; set; }

        /// <summary>
        /// Método para criar um novo ExceptionPayload de uma exceção de negócio.
        ///          
        /// As exceções de negócio, que são providas no NDD.Configuracoes.Portal.Domain
        /// são identificadas pelos códigos no enum ErrorCodes. 
        /// 
        /// Assim, esse método monta o ExceptionPayload, que será o código retornado o cliente, 
        /// com base na exceção lançada.
        /// 
        /// </summary>
        /// <param name="exception">É a exceção lançada</param>
        /// <param name="errorCode">Código HTTP de erro</param>
        /// <param name="failures">Lista de problemas de validação</param>
        /// <returns>ExceptionPayload contendo o código do erro e a mensagem da da exceção que foi lançada </returns>
        public static ExceptionPayload New<T>(T exception, int errorCode, IEnumerable<ValidationFailure>? failures = null) where T : Exception
        {
            return new ExceptionPayload
            {
                Codigo = errorCode,
                Mensagem = exception.Message,
                Erros = failures,
            };
        }
    }
}