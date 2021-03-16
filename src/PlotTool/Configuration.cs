using Microsoft.Extensions.Configuration;

namespace PlotTool
{
    internal static class Configuration
    {
        private static IConfiguration _configuration;

        public static IConfiguration Instance
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", false)
                        .Build();
                }

                return _configuration;
            }
        }
    }
}
