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
        //MainPageViewModel mainPageViewModel;

        public HomePage()
        {
            InitializeComponent();

            if (App.CurrentUser.Email != null && App.CurrentUser.Name != null)
            {
                Navigation.PushAsync(new BeverageSelectPage());
            }

            //LogoImage.Source = Device.RuntimePlatform == Device.Android ?
            //    ImageSource.FromFile("logo_placeholder.png") :
            //    ImageSource.FromFile("Images/logo_placeholder.png");

            //mainPageViewModel = new MainPageViewModel();
            //BindingContext = mainPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //mainPageViewModel.OnPageAppearingCommand.Execute(null);
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