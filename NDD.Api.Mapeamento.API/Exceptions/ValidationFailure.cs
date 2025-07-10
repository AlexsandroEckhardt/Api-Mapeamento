namespace NDD.Api.Mapeamento.API.Exceptions
{
    public class ValidationFailure
    {
        public string? Campo { get; set; }

        public string? MensagemDeErro { get; set; }

        public string? Correcao { get; set; }

    }
}
