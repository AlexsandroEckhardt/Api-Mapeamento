using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using NDD.Api.Mapeamento.API.Exceptions;
using NDD.Space.Base.Domain.Exceptions;

using System.Diagnostics.CodeAnalysis;

namespace NDD.Api.Mapeamento.API.Filters
{
    [ExcludeFromCodeCoverage]
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Método invocado quando ocorre uma exceção no controller
        /// </summary>
        /// <param name="context">É o contexto atual da requisição</param>
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = ErrorCodes.Unhandled.GetHashCode();
            context.Result = new JsonResult(ExceptionPayload.New(context.Exception, ErrorCodes.Unhandled.GetHashCode()));
        }
    }
}