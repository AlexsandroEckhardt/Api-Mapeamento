namespace NDD.Api.Mapeamento.Base.Configuracoes
{
    public class AuthSettings
    {
        #region SSO

        public string Authority { get; set; }

        public string Audience { get; set; }

        #endregion

        #region Identity

        public string AuthorityIdentity { get; set; }

        public string AudienceIdentity { get; set; }

        public string ClientIdIdentity { get; set; }

        public string ClientSecretIdentity { get; set; }

        #endregion

        public string SuperAdm { get; set; }
    }
}
