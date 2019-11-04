using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;

namespace prj3beerAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Beverage bev;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            bev = new Beverage("", "", 7);

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.status_screen);

            TextView tvIdealTemp = (TextView) FindViewById(Resource.Id.tvIdealTemp);

            try
            {
                tvIdealTemp.SetText(9); // bev.getIdealTemp()
            }
            catch(Exception e)
            {
                // nothing
            }
        }
    }
}

