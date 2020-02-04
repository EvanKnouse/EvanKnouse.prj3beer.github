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

        // Stores The Current User Name
        private const string UserKey = "user_key";
        // Default value for Name should be null
        private static readonly string DefaultUser = null;

        // Stores The User Email
        private const string EmailKey = "email_key";
        // Default value for Email should be null
        private static readonly string DefaultEmail = null;

        // Stores a boolean for showing the welcome modal
        private const string WelcomePromptKey = "welcome_key";
        // Default value for the Welcome Prompt (show)
        private static readonly bool WelcomePrompt = true;

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
        /// This Method is responsible for setting/updating the values for the Currently Logged in User Name
        /// </summary>
        public static string CurrentUserName
        {
            get
            {   // Gets the value stored in the UserKey, or the default user if it is not set.
                return AppSettings.GetValueOrDefault(UserKey, DefaultUser);
            }
            set
            {   // Update the UserKey with the passed in value
                AppSettings.AddOrUpdateValue(UserKey, value);
            }
        }

        /// <summary>
        /// This Method is responsible for setting/updating the values for the Currently Logged in User Email
        /// </summary>
        public static string CurrentUserEmail
        {
            get
            {   // Gets the value stored in the EmailKey, or the default user if it is not set.
                return AppSettings.GetValueOrDefault(EmailKey, DefaultEmail);
            }
            set
            {   // Update the EmailKey with the passed in value
                AppSettings.AddOrUpdateValue(EmailKey, value);
            }
        }

        public static string URLSetting
        {
            get
            {
                return AppSettings.GetValueOrDefault(BaseURL, BaseURLDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(BaseURL, value);
            }
        }

        /// <summary>
        /// This Method is responsible for setting/updating the promp key for a user returning to the Application.
        /// </summary>
        public static bool WelcomePromptSetting
        {
            get
            {   // Gets the value stored in the WelcomePromptKey, or the default user if it is not set.
                return AppSettings.GetValueOrDefault(WelcomePromptKey, WelcomePrompt);
            }
            set
            {   // Update the WelcomePromptKey with the passed in value
                AppSettings.AddOrUpdateValue(WelcomePromptKey, value);
            }
        }
    }
}