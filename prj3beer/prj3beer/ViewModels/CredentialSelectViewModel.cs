using Newtonsoft.Json;
using prj3beer.OAuth;
using prj3beer.OAuth.Services;
using prj3beer.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace prj3beer.ViewModels
{
    class CredentialSelectViewModel : BaseViewModel
    {
        OAuth2Authenticator oAuth2Authenticator;
        OAuth2ProviderType OAuth2ProviderType { get; set; }

        // where
        public static EventHandler OnPresenter;

        private void Authenticate(OAuth2ProviderType providerType)
        {
            oAuth2Authenticator = OAuthAuthenticatorHelper.CreateOAuth2(providerType);
            oAuth2Authenticator.Completed += OAuth2Authenticator_Completed;
            oAuth2Authenticator.Error += OAuth2Authenticator_Error;

            OAuth2ProviderType = providerType;
            var presenter = new OAuthLoginPresenter();
            // switch statement was here for Device.RuntimePlatform
            // reference documentation for IOS
            presenter.Login(oAuth2Authenticator);
        }

        #region COMMANDS
        public Command GoogleClickedCommand => new Command(GoogleClicked);
        #endregion

        #region AUTH COMPLETED
        private async void OAuth2Authenticator_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                try
                {
                    await SecureStorage.SetAsync(OAuth2ProviderType.ToString(), JsonConvert.SerializeObject(e.Account.Properties));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                string email = string.Empty;

                switch (OAuth2ProviderType)
                {
                    case OAuth2ProviderType.GOOGLE:
                        email = await ProviderService.GetGoogleEmailAsync();
                        break;
                }

                await SecureStorage.SetAsync("Email", email);
                await SecureStorage.SetAsync("Provider", OAuth2ProviderType.ToString());

                // TODO: see where this goes, when does it happen, why, who, what?
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                oAuth2Authenticator.OnCancelled();
                oAuth2Authenticator = default(OAuth2Authenticator);
            }
        }
        #endregion

        #region AUTH ERRORS
        private async void OAuth2Authenticator_Error(object sender, AuthenticatorErrorEventArgs e)
        {
            OAuth2Authenticator authenticator = (OAuth2Authenticator)sender;
            if (authenticator != null)
            {
                authenticator.Completed -= OAuth2Authenticator_Completed;
                authenticator.Error -= OAuth2Authenticator_Error;
            }

            string title = "Authentication Error";
            string msg = e.Message;

            Debug.WriteLine($"Error authenticating with {OAuth2ProviderType}! Message: {e.Message}");
            await Application.Current.MainPage.DisplayAlert(title, msg, "OK");
        }
        #endregion

        #region METHODS
        private void GoogleClicked()
        {
            Authenticate(OAuth2ProviderType.GOOGLE);
        }
        #endregion
    }
}