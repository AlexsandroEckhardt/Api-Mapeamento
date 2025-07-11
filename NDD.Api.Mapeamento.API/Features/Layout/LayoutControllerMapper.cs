using AutoMapper;

using NDD.Api.Mapeamento.Application.Dto.Layout;

namespace NDD.Api.Mapeamento.API.Features.Layout
{
    public class LayoutControllerMapper : Profile
    {
        public LayoutControllerMapper()
        {
            CreateMap<LayoutDto, LayoutViewModel>();
        }
    }
}

