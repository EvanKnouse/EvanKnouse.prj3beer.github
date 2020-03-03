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
        public CredentialSelectPage(bool isNew)
        {
            InitializeComponent();

            // Set the new User boolean to true or false from passed in value
            newUser = isNew;

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

            csvm.NavigateAway = false;

            bool loggedin = Settings.CurrentUserEmail != "";// && Settings.CurrentUserName != "";

            MessageLabel.Text = loggedin ? "Are You Sure You Want To Sign Out?" : "Sign In With";

            //TODO: Implement Facebook conditions
            if (loggedin)
            {
                FacebookButton.IsVisible = false;
                GoogleButton.IsVisible = false;
                YesButton.IsVisible = true;
                //csvm.NavigateAway = true;
            }
            else
            {
                FacebookButton.IsVisible = true;
                GoogleButton.IsVisible = true;
                YesButton.IsVisible = false;
            }

            //Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            //{
            //    if (csvm.NavigateAway)
            //    {
            //        Task.Run(async () =>
            //        {
            //            csvm.NavigateAway = false;
            //            await Navigation.PopModalAsync();
            //            return false;
            //        });
            //    }
            //    return true;
            //});
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
    }
}