using FluentValidation;

using MediatR;

using NDD.Space.Base.Domain;

using System.Diagnostics.CodeAnalysis;

namespace NDD.Api.Mapeamento.API.Behaviours
{
    [ExcludeFromCodeCoverage]
    public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<Exception, TResponse>>
                                                           where TRequest : IRequest<Result<Exception, TResponse>>
    {
        private readonly IValidator<TRequest>[] _validators;

        public ValidationPipeline(IValidator<TRequest>[] validators)
        {
            _validators = validators;
        }

        public async Task<Result<Exception, TResponse>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<Exception, TResponse>> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                return new ValidationException(failures);
            }

            return await next();
        }

        public async Task<Result<Exception, TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<Exception, TResponse>> next, CancellationToken cancellationToken)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                return new ValidationException(failures);
            }

            return await next();
        }
    }
}
