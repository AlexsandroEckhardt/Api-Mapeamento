using AutoMapper;

using NDD.Api.Mapeamento.Application.Dto.Mapeamento;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Requisicao;
using NDD.Api.Mapeamento.Domain.Features.Mapeamento.Resposta;

namespace NDD.Api.Mapeamento.Application.Features.Mapeamento
{
    public class MapeamentoApplicationMapper : Profile
    {
        public MapeamentoApplicationMapper()
        {
            CreateMap<MapeamentoCommand, MapeamentoRequisicao>();
            CreateMap<MapeamentoResposta, MapeamentoDto>();
        }
    }
}