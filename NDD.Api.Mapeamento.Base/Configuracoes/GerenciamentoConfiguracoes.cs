namespace NDD.Api.Mapeamento.Base.Configuracoes
{
    public class GerenciamentoConfiguracoes
    {
        public AppSettings? AppSettings { get; set; }

        public AuthSettings? AuthSettings { get; set; }

        public string? CaminhoArquivoLog { get; set; }

        public string? CaminhoMiddlewareXml { get; set; }

        public string? CaminhoConfiguracaoThrottling { get; set; }
    }
}
