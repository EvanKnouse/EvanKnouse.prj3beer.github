using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace prj3beer.Models
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string TemperatureKey = "temperature_key";
        private static readonly bool SettingsDefault = true;

        #endregion


        public static bool TemperatureSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(TemperatureKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(TemperatureKey, value);
            }
        }
    }

}
