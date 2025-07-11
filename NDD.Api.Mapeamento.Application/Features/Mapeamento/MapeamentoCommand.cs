using FluentValidation;

using MediatR;

using NDD.Api.Mapeamento.Application.Dto.Mapeamento;
using NDD.Api.Mapeamento.Base.Configuracoes;
using NDD.Space.Base.Domain;

namespace NDD.Api.Mapeamento.Application.Features.Mapeamento
{
    public class MapeamentoCommand : IRequest<Result<Exception, MapeamentoDto>>
    {
        public string MapeamentoOrigem { get; set; }
        public string MapeamentoDestino { get; set; }
        public string Documento { get; set; }

        [SwaggerIgnore]
        public string? IdentificadorCliente { get; set; }
    }

    public class DadosValidator : AbstractValidator<MapeamentoCommand>
    {
        public DadosValidator()
        {
            RuleFor(x => x.MapeamentoOrigem).NotNull().WithMessage("Campo obrigatório não informado")
                                          .WithErrorCode("Mapeamento de Origem deve ser informado!");

            RuleFor(x => x.MapeamentoDestino).NotNull().WithMessage("Campo obrigatório não informado")
                                          .WithErrorCode("Mapeamento de Destino deve ser informado!");

            RuleFor(x => x.Documento).NotNull().WithMessage("Campo obrigatório não informado")
                                          .WithErrorCode("Documento deve ser informado!");
        }
    }
}
