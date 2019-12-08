using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            string apkPath = "D:\\COSACPMG\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debugcom.companyname.prj3beer.apk";

            IApp app;

            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.ApkFile(apkPath).StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}