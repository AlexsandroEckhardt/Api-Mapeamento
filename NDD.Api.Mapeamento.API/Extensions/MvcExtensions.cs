using FluentValidation;
using FluentValidation.AspNetCore;

using NDD.Api.Mapeamento.API.Filters;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using System.Diagnostics.CodeAnalysis;

namespace NDD.Api.Mapeamento.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class MvcExtensions
    {
        public static void AddFilters(this IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add(new ExceptionHandlerAttribute()));
        }

        public static void AddMVC(this IServiceCollection services)
        {
            services.AddMvc(x => x.EnableEndpointRouting = false)
            .AddFluentValidation()
            .AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.Formatting = Formatting.None;
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                opt.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddValidatorsFromAssemblyContaining<Application.AppModule>();
        }
    }
}
