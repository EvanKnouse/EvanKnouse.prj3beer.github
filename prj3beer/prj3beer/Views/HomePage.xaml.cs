using prj3beer.Models;
using prj3beer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prj3beer.Views
{
    /// <summary>
    /// This is the main screen that starts up from the app screen
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            // If there is a currently saved user
            if (Settings.CurrentUserName != null && Settings.CurrentUserEmail != null)
            {   // Navigate straight to the Beverage Select Page
                // You do not need to log in or sign up again
                Navigation.PushAsync(new BeverageSelectPage());
            }

        }

        /// <summary>
        /// This method is called when either the sign in or sign up button is pressed.
        /// It uses the text from within the button to determine what is passed in to the 
        /// CredentialSelectPage
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Event</param>
        private void ButtonClicked(object sender, EventArgs e)
        {
            // Get the string from the Button
            string buttonText = ((Button)sender).Text;

            // Switch statement based on the button text
            switch (buttonText)
            {
                case "Sign Up":
                    // If the sign up button is pushed, pass in true to the CredentialSelectPage
                    Navigation.PushAsync(new CredentialSelectPage(true));
                    break;

                case "Sign In":
                    // If the sign in button is pushed, pass in false to the CredentialSelectPage
                    Navigation.PushAsync(new CredentialSelectPage(false));
                    break;
            }
        }
    }
}