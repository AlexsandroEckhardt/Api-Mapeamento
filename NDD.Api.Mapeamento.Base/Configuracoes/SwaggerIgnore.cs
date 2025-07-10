namespace NDD.Api.Mapeamento.Base.Configuracoes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class SwaggerIgnoreAttribute : Attribute
    { }
}
