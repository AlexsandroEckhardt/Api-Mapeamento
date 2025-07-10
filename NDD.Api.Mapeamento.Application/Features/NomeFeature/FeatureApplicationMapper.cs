using AutoMapper;

using NDD.Api.Mapeamento.Application.Dto.Feature;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Requisicao;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Resposta;

namespace NDD.Api.Mapeamento.Application.Features.NomeFeature
{
    public class FeatureApplicationMapper : Profile
    {
        public FeatureApplicationMapper()
        {
            CreateMap<FeatureCommand, NomeFeatureRequisicao>();
            CreateMap<NomeFeatureResposta, FeatureDto>();
        }
    }
}