using AutoMapper;

using NDD.Api.Mapeamento.Application.Dto.Mapeamento;

namespace NDD.Api.Mapeamento.API.Features.Mapeamento
{
    public class MapeamentoControllerMapper : Profile
    {
        public MapeamentoControllerMapper()
        {
            CreateMap<MapeamentoDto, MapeamentoViewModel>();
        }
    }
}

