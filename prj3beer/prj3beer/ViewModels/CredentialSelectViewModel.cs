using System;
using System.ComponentModel;
using System.Windows.Input;

using prj3beer.Models;

using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;

using Xamarin.Forms;

namespace prj3beer.ViewModels
{
    public class CredentialSelectViewModel : INotifyPropertyChanged
    {
        public UserProfile User { get; set; } = new UserProfile();

        public string Name { get => User.Name; set => User.Name = value; }
                     
        public string Email { get => User.Email; set => User.Email = value; }

        public Uri Picture{ get => User.Picture; set => User.Picture = value; }

        public bool IsLoggedIn { get; set; }

        public string Token { get; set; }

        public ICommand LoginCommand { get; set; }

        public ICommand LogoutCommand { get; set; }

        private readonly IGoogleClientManager _googleClientManager;

        public event PropertyChangedEventHandler PropertyChanged;



        public CredentialSelectViewModel()
        {
            LoginCommand = new Command(LoginAsync);

            LogoutCommand = new Command(Logout);

            _googleClientManager = CrossGoogleClient.Current;

            IsLoggedIn = false;
        }

        public async void LoginAsync()
        {
            _googleClientManager.OnLogin += OnLoginCompleted;

			try 
			{
                await _googleClientManager.LoginAsync();
			}
			catch (GoogleClientSignInNetworkErrorException e)
			{
				await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
			}
			catch (GoogleClientSignInCanceledErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
			catch (GoogleClientSignInInvalidAccountErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
			catch (GoogleClientSignInInternalErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientNotInitializedErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
			catch (GoogleClientBaseException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
        }

        private void OnLoginCompleted(object sender, GoogleClientResultEventArgs<GoogleUser> loginEventArgs)
        {
            if (loginEventArgs.Data != null)
            {
                GoogleUser googleUser = loginEventArgs.Data;

                User.Name = googleUser.Name;

                User.Email = googleUser.Email;

                User.Picture = googleUser.Picture;

                var GivenName = googleUser.GivenName;

                var FamilyName = googleUser.FamilyName;

                // Debug.WriteLine(User.Email);

                IsLoggedIn = true;

				var token = CrossGoogleClient.Current.ActiveToken;

				Token = token;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error", loginEventArgs.Message, "OK");
            }

            _googleClientManager.OnLogin -= OnLoginCompleted;

        }

        public void Logout()
        {
            _googleClientManager.OnLogout += OnLogoutCompleted;
            _googleClientManager.Logout();
        }

        private void OnLogoutCompleted(object sender, EventArgs loginEventArgs)
        {
            IsLoggedIn = false;
            User.Email = "Offline";
            _googleClientManager.OnLogout -= OnLogoutCompleted;
        }
    }
}
   /* class CredentialSelectViewModel : BaseViewModel
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
}*/