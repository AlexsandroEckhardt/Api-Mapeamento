using AutoMapper;

using NDD.Api.Mapeamento.API.Features.Layout;
using NDD.Api.Mapeamento.Application.Features.Layout;

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
                mc.AddProfile(new LayoutApplicationMapper());
                //mc.AddProfile(new MapeamentoControllerMapper());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
