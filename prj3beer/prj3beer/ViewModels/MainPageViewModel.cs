using Newtonsoft.Json;
using prj3beer.OAuth;
using prj3beer.OAuth.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace prj3beer.ViewModels
{
    class MainPageViewModel : BaseViewModel
    {
        private string userEmail;
        private string providerName;
        private OAuth2ProviderType provider;
        private string providerToken;

        public string UserEmail { get => userEmail; set => SetProperty(ref userEmail,value); }

        public string ProviderName { get => providerName; set => SetProperty(ref providerName, value); }
        public OAuth2ProviderType Provider { get => provider; set => SetProperty(ref provider, value); }
        public string ProviderToken { get => providerToken; set => SetProperty(ref providerToken, value); }


        public GoogleToken GoogleCredentials { get; set; }

        public Command OnPageAppearingCommand => new Command(ExecuteOnPageAppearing);

        public async void ExecuteOnPageAppearing()
        {
            UserEmail = await SecureStorage.GetAsync("Email");
            string socialProvider = await SecureStorage.GetAsync("Provider");

            OAuth2ProviderType provider = (OAuth2ProviderType)Enum.Parse(typeof(OAuth2ProviderType), socialProvider);

            var token = string.Empty;

            switch(provider)
            {
                case OAuth2ProviderType.GOOGLE:
                    token = await SecureStorage.GetAsync(provider.ToString());
                    GoogleCredentials = JsonConvert.DeserializeObject<GoogleToken>(token);
                    ProviderToken = GoogleCredentials.AccessToken;
                    ProviderName = OAuth2ProviderType.GOOGLE.ToString();
                    break;
            }


        }
    }
}
