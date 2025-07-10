using Microsoft.AspNetCore;

using System.Diagnostics.CodeAnalysis;

namespace NDD.Api.Mapeamento.API
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public const string PLATAFORMA_NDD_APPSETTINGS_PROFILE = "PLATAFORMA_NDD_APPSETTINGS_PROFILE";

        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)                          
                          .ConfigureAppConfiguration(config =>
                          {
                              string? appSettingsProfile = Environment.GetEnvironmentVariable(PLATAFORMA_NDD_APPSETTINGS_PROFILE);

                              string appSettings = string.IsNullOrWhiteSpace(appSettingsProfile) ? "appsettings.json" : Path.Combine("Desenvolvimento", $"appsettings_{appSettingsProfile}.json");

                              config.AddJsonFile(appSettings, optional: true);
                              config.AddEnvironmentVariables();

                              if (args != null && args.Length > 0)
                                  config.AddCommandLine(args);
                          })
                          .UseStartup<Startup>();

        }

    }
}