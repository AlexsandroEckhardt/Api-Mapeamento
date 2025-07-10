
using Microsoft.OpenApi.Models;

using NDD.Api.Mapeamento.Base.Configuracoes;

using Swashbuckle.AspNetCore.SwaggerGen;

using System.Reflection;

namespace NDD.Api.Mapeamento.API.Filters.Swaggers
{
    public class SwaggerSkipPropertyFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }

            var ignorarPropriedades = context.Type.GetProperties().Where(t => t.GetCustomAttribute<SwaggerIgnoreAttribute>() != null);

            foreach (var propriedadeIgnorada in ignorarPropriedades)
            {
                var ignorar = schema.Properties.Keys.SingleOrDefault(x => string.Equals(x, propriedadeIgnorada.Name, StringComparison.OrdinalIgnoreCase));

                if (ignorar != null)
                {
                    schema.Properties.Remove(ignorar);
                }
            }
        }
    }
}

