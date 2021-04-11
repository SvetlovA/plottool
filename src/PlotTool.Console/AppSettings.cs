using Microsoft.Extensions.Configuration;

namespace PlotTool.Console
{
    public class AppSettings
    {
        private static AppSettings _appSettings;

        public static AppSettings Instance
        {
            get
            {
                if (_appSettings == null)
                {
                    _appSettings = new AppSettings();
                    Configuration.Instance.Bind(_appSettings);
                }

                return _appSettings;
            }
        }

        public string PlotName { get; set; }
        public string[] PlotPaths { get; set; }
    }
}
