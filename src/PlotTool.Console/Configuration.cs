using Microsoft.Extensions.Configuration;

namespace PlotTool.Console
{
    internal static class Configuration
    {
        private static IConfiguration _configuration;

        public static IConfiguration Instance =>
            _configuration ??= new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();
    }
}
