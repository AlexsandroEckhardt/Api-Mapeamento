using FluentValidation;

using MediatR;

using NDD.Api.Mapeamento.Application.Dto.Layout;
using NDD.Api.Mapeamento.Base.Configuracoes;
using NDD.Space.Base.Domain;

namespace NDD.Api.Mapeamento.Application.Features.Layout
{
    public class LayoutCommand : IRequest<Result<Exception, LayoutDto>>
    {
        public string LayoutOrigem { get; set; }
        public string LayoutDestino { get; set; }
        public string Documento { get; set; }

        [SwaggerIgnore]
        public string? IdentificadorCliente { get; set; }
    }

    public class DadosValidator : AbstractValidator<LayoutCommand>
    {
        public DadosValidator()
        {
            RuleFor(x => x.LayoutOrigem).NotNull().WithMessage("Campo obrigatório não informado")
                                          .WithErrorCode("Layout de Origem deve ser informado!");

            RuleFor(x => x.LayoutDestino).NotNull().WithMessage("Campo obrigatório não informado")
                                          .WithErrorCode("Layout de Destino deve ser informado!");

            RuleFor(x => x.Documento).NotNull().WithMessage("Campo obrigatório não informado")
                                          .WithErrorCode("Documento deve ser informado!");
        }
    }
}
