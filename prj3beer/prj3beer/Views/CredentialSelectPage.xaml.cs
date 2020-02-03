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
    public partial class CredentialSelectPage : ContentPage
    {
        CredentialSelectViewModel csvm;
        public static bool newUser;

        public CredentialSelectPage(bool isNew)
        {
            InitializeComponent();

            newUser = isNew;

            BindingContext = csvm = new CredentialSelectViewModel();

            MessageLabel.Text = newUser ? "Sign Up With:" : "Sign In With:";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(csvm.Email != null && csvm.Name != null)
            {
                Navigation.PushAsync(new BeverageSelectPage());
            }
        }
    }
}