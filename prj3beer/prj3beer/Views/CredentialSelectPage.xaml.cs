using prj3beer.Models;
using prj3beer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prj3beer.Views
{
    /// <summary>
    /// This class contains the elements for the user to Sign In/Up with Google Credentials
    /// This page will contain other log in methods potentially in the future.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CredentialSelectPage : ContentPage
    {
        // View Model containing all user information and google Oauth Methods
        public static CredentialSelectViewModel csvm;
        // Boolean to see if this is a new sign up, or exisiting sign in.
        public static bool newUser;

        /// <summary>
        /// Default constructor, takes in the boolean from the previous screen
        /// </summary>
        /// <param name="isNew"></param>
        public CredentialSelectPage()
        {
            InitializeComponent();

            // Set the new User boolean to true or false from passed in value
            //newUser = isNew;

            // Set the binding context of the View to the csvm, and instantiate it
            BindingContext = csvm = new CredentialSelectViewModel();

            // Set the message label to different text depending on the passed in boolean.
            //MessageLabel.Text = isNew ? "Sign Up With" : "Sign In With";
            //MessageLabel.Text = "Sign In With";
        }

        /// <summary>
        /// This method gets called whenever this page is visited
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Boolean to keep track of a signed in/out user.
            bool loggedIn = Settings.CurrentUserEmail.Length == 0;

            // Change the message label based on a user being logged in or out.
            MessageLabel.Text = loggedIn ? "Are You Sure You Want To Sign Out?" : "Sign In With";

            // If user is logged in
            if (loggedIn)
            {   // Hide the Facebook + Google Buttons
                FacebookButton.IsVisible = false;
                GoogleButton.IsVisible = false;
                // Show the Yes button
                YesButton.IsVisible = true;
            }
            else
            {   // Show the Facebook + Google Buttons
                FacebookButton.IsVisible = true;
                GoogleButton.IsVisible = true;
                // Hide the Yes Button
                YesButton.IsVisible = false;
            }
        }

        /// <summary>
        /// Event handler for the persistent cancel button on the sign in/out screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            //Close the Sign in/out screen
            Navigation.PopModalAsync();
        }

        /* NOTE */
        /* ENABLE THIS TO SEND USERS BACK TO THE START PAGE */
        /* AFTER SIGNING IN OR OUT OF GOOGLE/FACEBOOK */
        /* 
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Application.Current.MainPage = new NavigationPage(new BeverageSelectPage());
        }
        */
    }
}