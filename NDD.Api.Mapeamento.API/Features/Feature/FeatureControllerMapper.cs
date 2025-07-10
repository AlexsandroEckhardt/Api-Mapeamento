using AutoMapper;

using NDD.Api.Mapeamento.Application.Dto.Feature;

namespace NDD.Api.Mapeamento.API.Features.Feature
{
    public class FeatureControllerMapper : Profile
    {
        public FeatureControllerMapper()
        {
            CreateMap<FeatureDto, FeatureViewModel>();
        }
    }
}

