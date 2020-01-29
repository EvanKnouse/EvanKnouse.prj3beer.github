using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace prj3beer.Models
{
    /// <summary>
    /// This Class holds all of the persistant settings in the beer app
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        //Constant values to store the Key and default values for the Settings Dictionary
        #region Setting Constants

        private const string TemperatureKey = "temperature_key";
        private static readonly bool TemperatureDefault = true;

        private const string BaseURL = "base_url";
        private static readonly string BaseURLDefault = @"http://my-json-server.typicode.com/prj3beer/prj3beer-api";

        private const string NotificationKey = "notification_key";
        private static readonly bool NotificationDefault = true;

        private const string InRangeKey = "inrange_key";
        private static readonly bool InRangeDefault = true;

        private const string TooHotColdKey = "toohotcold_key";
        private static readonly bool TooHotColdDefault = true;

        #endregion

        /// <summary>
        /// This attribute gets or sets the temperature unit settings, 
        /// </summary>
        public static bool TemperatureSettings
        {
            get
            {
                //Returns true for Celcius and false for Fahtrenheit, if Temperature was never set its default is true 
                return AppSettings.GetValueOrDefault(TemperatureKey, TemperatureDefault);
            }
            set
            {
                //Sets the TemperatureSettings KeyValue pair and sets its value to the passed in value
                AppSettings.AddOrUpdateValue(TemperatureKey, value);
            }
        }

        /// <summary>
        /// This attribute gets or sets the url to use for API interactions
        /// </summary>
        public static string URLSetting
        {
            get
            {
                //Gets the url provided if possible, otherwise use the default url
                return AppSettings.GetValueOrDefault(BaseURL, BaseURLDefault);
            }
            set
            {
                //Sets the APIManager url to use for API interactions
                AppSettings.AddOrUpdateValue(BaseURL, value);
            }
        }

        /// <summary>
        /// This attribute gets or sets the master notification settings
        /// </summary>
        public static bool NotificationSettings
        {
            get
            {
                //Returns true or false (essentially IsToggled property) value for master notification switch
                //Otherwise returns default, which is true (IsToggled = true)
                return AppSettings.GetValueOrDefault(NotificationKey, NotificationDefault);
            }
            set
            {
                //Set the NotificationSettings key-value pair sets its value to the passed in value
                AppSettings.AddOrUpdateValue(NotificationKey, true);
            }
        }

        /// <summary>
        /// This attribute gets or sets the in-range notification subsettings
        /// </summary>
        public static bool InRangeSettings
        {
            get
            {
                //Returns true or false (essentially IsToggled property) value for in-range notification switch
                //Otherwise returns default, which is true (IsToggled = true)
                return AppSettings.GetValueOrDefault(InRangeKey, InRangeDefault);
            }
            set
            {
                //Set the InRangeSettings key-value pair sets its value to the passed in value
                AppSettings.AddOrUpdateValue(InRangeKey, true);
            }
        }

        /// <summary>
        /// This attribute gets or sets the too hot/cold notification subsettings
        /// </summary>
        public static bool TooHotColdSettings
        {
            get
            {
                //Returns true or false (essentially IsToggled property) value for too hot/cold notification switch
                //Otherwise returns default, which is true (IsToggled = true)
                return AppSettings.GetValueOrDefault(TooHotColdKey, TooHotColdDefault);
            }
            set
            {
                //Set the TooHotColdSettings key-value pair sets its value to the passed in value
                AppSettings.AddOrUpdateValue(TooHotColdKey, true);
            }
        }
    }
}