using AutoMapper;

using NDD.Api.Mapeamento.Application.Dto.Layout;
using NDD.Api.Mapeamento.Domain.Features.Layout.Requisicao;
using NDD.Api.Mapeamento.Domain.Features.Layout.Resposta;

namespace NDD.Api.Mapeamento.Application.Features.Layout
{
    public class LayoutApplicationMapper : Profile
    {
        public LayoutApplicationMapper()
        {
            CreateMap<LayoutCommand, LayoutRequisicao>();
            CreateMap<LayoutResposta, LayoutDto>();
        }
    }
}