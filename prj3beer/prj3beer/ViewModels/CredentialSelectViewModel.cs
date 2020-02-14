using System;
using System.ComponentModel;
using System.Windows.Input;

using prj3beer.Models;

using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;

using Xamarin.Forms;

namespace prj3beer.ViewModels
{
    /// <summary>
    /// This is all of the functionality for OAuth.
    /// That takes place on the Credential Select View Model.
    /// Currently Only the Google Sign In is set up in the application.
    /// </summary>
    public class CredentialSelectViewModel : INotifyPropertyChanged
    {
        // Creates a new UserProfile, set up with a getter/setter for OAuth
        public UserProfile User { get; set; } = new UserProfile();

        // Getter/Setter Attribute for the User's Name
        public string Name { get => User.Name; set => User.Name = value; }

        // Getter/Setter Attribute for the User's Email
        public string Email { get => User.Email; set => User.Email = value; }

        // Getter/Setter Attribute for the User's Picture
        public Uri Picture{ get => User.Picture; set => User.Picture = value; }

        // Getter/Setter Attribute for a logged in status
        public bool IsLoggedIn { get; set; }

        // Google's Token - Persistent for 1 hour
        public string Token { get; set; }

        // ICommand that triggers when the User attempts to Log In
        public ICommand LoginCommand { get; set; }

        // Icommand that will trigger when the User logs out
        public ICommand LogoutCommand { get; set; }

        // Private Interfaced GoogleClientManager
        // This interface enforces that elements on this ViewModel use certain methods.
        private readonly IGoogleClientManager _googleClientManager;

        // Event handler for properties changing
        public event PropertyChangedEventHandler PropertyChanged;

        public bool NavigateAway { get; set; }

        // Constructor for the Credential View Model
        public CredentialSelectViewModel()
        {
            // Initialize the LogInCommand command
            LoginCommand = new Command(LoginAsync);

            // Implement in the future for logging a user out!
            LogoutCommand = new Command(Logout);

            // Instantiate the googleClientManager
            _googleClientManager = CrossGoogleClient.Current;

            // Boolean for if the user is currently Logged in or not
            IsLoggedIn = false;
        }

        // This method is called using the LogoutCommand
        public void Logout()
        {
            _googleClientManager.OnLogout += OnLogoutCompleted;
            _googleClientManager.Logout();
        }

        private void OnLogoutCompleted(object sender, EventArgs loginEventArgs)
        {
            IsLoggedIn = false;
            User.Email = "";
            Settings.CurrentUserEmail = "";
            Settings.CurrentUserName = "";

            NavigateAway = true;

            _googleClientManager.OnLogout -= OnLogoutCompleted;
        }

        // This method is called using the LoginCommand
        public async void LoginAsync()
        {
            // Add the Event Handler to the GoogleClient Manager's on login property
            _googleClientManager.OnLogin += OnLoginCompleted;
            
			try
            {   // Try to use the Google Client Manager to Login Asyncronously
                await _googleClientManager.LoginAsync();
			}
			catch (GoogleClientSignInNetworkErrorException error)
			{   // This error will trigger when there is a network error
				await App.Current.MainPage.DisplayAlert("Error", error.Message, "OK");
			}
			catch (GoogleClientSignInCanceledErrorException error)
            {   // This error will trigger when a user cancells a sign in/up process
                await App.Current.MainPage.DisplayAlert("Error", error.Message, "OK");
            }
			catch (GoogleClientSignInInvalidAccountErrorException error)
            {   // This error will trigger on invalid credentials being entered
                await App.Current.MainPage.DisplayAlert("Error", error.Message, "OK");
            }
			catch (GoogleClientSignInInternalErrorException error)
            {   //This error shouldn't trigger from our app, but from a google server error
                await App.Current.MainPage.DisplayAlert("Error", error.Message, "OK");
            }
            catch (GoogleClientNotInitializedErrorException error)
            {   // This error will trigger if there is an error with out OAuth Credentials
                await App.Current.MainPage.DisplayAlert("Error", error.Message, "OK");
            }
			catch (GoogleClientBaseException error)
            {   // This should catch all other errors
                await App.Current.MainPage.DisplayAlert("Error", error.Message, "OK");
            }
        }

        /// <summary>
        /// This method is called when a user has completed the log in process
        /// </summary>
        /// <param name="loginEventArgs">The event that is triggered</param>
        private void OnLoginCompleted(object sender, GoogleClientResultEventArgs<GoogleUser> loginEventArgs)
        {
            // If the compelted event data is not null (which it shouldn't be if the user has logged in)
            if (loginEventArgs.Data != null)
            {   // Create a new googleUser using the returned data from the event
                GoogleUser googleUser = loginEventArgs.Data;

                // Store the returned name to the local user
                User.Name = googleUser.Name;
                // As well as store it in the settings
                Settings.CurrentUserName = googleUser.Name;

                // Store the returned email to the local user
                User.Email = googleUser.Email;
                // As well as store it in settings
                Settings.CurrentUserEmail = googleUser.Email;

                // Store the user's picture to the local user 
                User.Picture = googleUser.Picture;
                // Did not implement in permanent storage - we don't display it anywhere
               
                // Change the logged in boolean to true
                IsLoggedIn = true;

                // You will be welcomed after sign in
                Settings.WelcomePromptSetting = true;

                // Set the token to the Active Token from the Cross Google Client
                Token = CrossGoogleClient.Current.ActiveToken;

                NavigateAway = true;
            }
            else
            {   // If there is an issue retriving data from the returned event
                App.Current.MainPage.DisplayAlert("Error", loginEventArgs.Message, "OK");
            }

            // Removes (unsubscribes) the event handler from the GoogleClientHandler
            _googleClientManager.OnLogin -= OnLoginCompleted;
        }
    }
}
