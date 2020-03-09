using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;

[assembly: Dependency(typeof(prj3beer.Droid.AndroidToasts))]
namespace prj3beer.Droid
{
    class AndroidToasts : Services.iToastHandler
    {
        #region story 52/29 Favorites
        
            public void longtoast(string msg)
            {
                Toast.MakeText(AndroidApp.Context, msg, ToastLength.Long).Show();
            }

            public void shortToast(string msg)
            {
                Toast.MakeText(AndroidApp.Context, msg, ToastLength.Short).Show();
            }
        
        #endregion
    }
}