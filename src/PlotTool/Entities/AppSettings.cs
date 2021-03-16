﻿using Microsoft.Extensions.Configuration;

namespace PlotTool.Entities
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

        public string[] PlotDirectoryPaths { get; set; }
    }
}
