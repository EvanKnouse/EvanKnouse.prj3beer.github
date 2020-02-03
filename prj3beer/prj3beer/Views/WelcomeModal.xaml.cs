using prj3beer.Models;
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
    /// This View displays a welcome message to the current user depending on if they are a new user or existing user.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomeModal : ContentPage
    {
        public WelcomeModal()
        {
            InitializeComponent();

            // Set the welcome message to "Welcome" or "Welcome Back" + <User Name>
            welcomeLabel.Text = (CredentialSelectPage.newUser ? "Welcome " : "Welcome back ") + Settings.CurrentUserName;
        }

        private void ContinueButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}