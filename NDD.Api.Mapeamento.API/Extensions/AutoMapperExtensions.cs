using AutoMapper;

using NDD.Api.Mapeamento.API.Features.Feature;
using NDD.Api.Mapeamento.Application.Features.NomeFeature;

using System.Diagnostics.CodeAnalysis;

namespace NDD.Api.Mapeamento.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class AutoMapperExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new FeatureApplicationMapper());
                mc.AddProfile(new FeatureControllerMapper());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
