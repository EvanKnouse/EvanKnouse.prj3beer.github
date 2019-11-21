using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.ApkFile(@"D:\virpc\prj3beer\prj3.beer\prj3beer.Android\bin\Debug\com.companyname.prj3beer.apk").StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}