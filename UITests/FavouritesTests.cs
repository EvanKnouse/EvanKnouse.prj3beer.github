using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    [TestFixture(Platform.Android)]
    public class FavouritesTests
    {
        string apkPath = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        IApp app;
        Platform platform;

        public FavouritesTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
            app = ConfigureApp.Android.ApkFile(apkPath).StartApp();
            // tap on the hamburger menu
            app.TapCoordinates(150, 90);
            // tap to navigate to the beverage select screen
            app.Tap("Beverage Select");
        }

        [Test]
        public void TestThatFavouriteButtonIsOnStatusScreen()
        {
            
        }

        [Test]
        public void TestThatPressingTheFavouriteButtonAddsTheBeverageAsAFavourite()
        {

        }

        [Test]
        public void TestThatPressingTheFavouriteButtonOnAFavouriteBeverageRemovesItAsAFavourite()
        {

        }

        [Test]
        public void TestThatSelectingAFavouriteBeverageFromTheBeverageSelectPageShowsTheFavouriteButtonToggled()
        {

        }

        [Test]
        public void TestThatMultipleBeveragesCanBeSelectedAsFavourites()
        {

        }
    }
}