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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomeModal : ContentPage
    {
        public WelcomeModal(bool newUser, string name)
        {
            InitializeComponent();

            welcomeLabel.Text = (newUser ? "Welcome, " : "Welcome back, ") + name + ", to the app...";
        }

        private void ContinueButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}