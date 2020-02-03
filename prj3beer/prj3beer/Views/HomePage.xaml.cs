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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            if (Settings.CurrentUserName != null && Settings.CurrentUserEmail != null)
            {
                Navigation.PushAsync(new BeverageSelectPage());
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string buttonText = ((Button)sender).Text;

            switch(buttonText)
            {
                case "Sign Up":
                    Navigation.PushAsync(new CredentialSelectPage(true));
                    break;
                case "Sign In":
                    Navigation.PushAsync(new CredentialSelectPage(false));
                    break;
            }
        }
    }
}