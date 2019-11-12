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
    class story10SignupPage
    {


        //https://docs.microsoft.com/en-us/appcenter/test-cloud/uitest/
        //PressEnter -  Press the enter key in the app.
        //Tap -  Simulates a tap / touch gesture on the matched element.
        //EnterText - Enters text into the view. In an iOS application, Xamarin.UITest will enter the text using the soft keyboard.
        //In contrast, Xamarin.UITest will not use the Android keyboard, it will directly enter the text into the view.
        //WaitForElement - Pauses the execution of the test until the views appear on the screen.
        //Screenshot(String) - Takes a screenshot of the application in it's current state and saves it to disk. It returns a FileInfo object with information about the screenshot taken.
        //Flash - This method will cause the selected view to "flash" or "flicker" on the screen.


        [TestFixture(Platform.Android)]
        public class Tests
        {
            IApp app;
            Platform platform;

            public Tests(Platform platform)
            {
                this.platform = platform;
            }

            [SetUp]
            public void BeforeEachTest()
            {
                //app = AppInitializer.StartApp(platform);
                app = ConfigureApp.Android.ApkFile(@"D:\prj3beer\prj3.beer\prj3beer\prj3beer.Android\bin\Debug\com.companyname.prj3beer.apk").StartApp();
            }


            [Test]
            public void signUpPageIsLoaded()
            {
                AppResult[] results = app.Query("signUpPage");
                Assert.IsTrue(results.Any());
            }

            [Test]
            public void signUpPageHasEmailEntry()
            {
                AppResult[] email = app.Query("entryEmail");
                //Will be greater then 0 if it exists, returns AppResult[]
               Assert.IsTrue(email.Any());
            }

            [Test]
            public void signUpPageHasNameEntry()
            {
                AppResult[] name = app.Query("entryName");
                //Will be greater then 0 if it exists, returns AppResult[]
                Assert.IsTrue(name.Any());
            }

            [Test]
            public void signUpPageHasPasswordEntry()
            {
                AppResult[] password = app.Query("entryPassword");
                //Will be greater then 0 if it exists, returns AppResult[]
                Assert.IsTrue(password.Any());
            }

            [Test]
            public void signUpPageHasAgeEntry()
            {
                AppResult[] age = app.Query("datePickerAge");
                //Will be greater then 0 if it exists, returns AppResult[]
                Assert.IsTrue(age.Any());
            }

            [Test]
            public void signUpPageHasGenderEntry()
            {
                AppResult[] gender = app.Query("pickerGender");
                //Will be greater then 0 if it exists, returns AppResult[]
                Assert.IsTrue(gender.Any());
            }

            [Test]
            public void signUpPageHasButton()
            {

                AppResult[] button = app.Query("signUpButton");
                //Will be greater then 0 if it exists, returns AppResult[]
                Assert.IsTrue(button.Any());
            }



            [Test]
            public void emailEntryCanBeTypedIn()
            {
                app.EnterText(c => c.Marked("entryEmail"), "jimBob@hotmail.com");

                String email = app.Query(("entryEmail"))[0].Text;

                Assert.AreEqual("jimBob@hotmail.com", email);
            }

            [Test]
            public void nameEntryCanBeTypedIn()
            {
                app.EnterText(c => c.Marked("entryName"), "Username");

                String name = app.Query(("entryName"))[0].Text;

                Assert.AreEqual("Username", name);

             
            }

            [Test]
            public void passwordEntryCanBeTypedIn()
            {
                app.EnterText(c => c.Marked("entryPassword"), "P@ssw0rd12");

                String password = app.Query(("entryPassword"))[0].Text;

                Assert.AreEqual("P@ssw0rd12", password);
            }

            [Test]
            public void ageCanBeSelected()
            {
                app.Tap("datePickerAge");

                //Minus todays date from the datePicker to get users age

                DateTime todayDate = DateTime.Now;
                
                app.Tap("2019");
               // app.ScrollUpTo("2000");
                app.ScrollDownTo("2000");
                app.Tap("2000");


                String birthday = app.Query("datePickerAge")[0].Text;


                DateTime dtBirthday = Convert.ToDateTime(birthday);

                long age = (todayDate - dtBirthday).Ticks;
                //60 60 24 365
                age /= 31536000; //should convert to years 

                Assert.AreEqual(19, (int)age);
            }

            [Test]
            public void genderCanBeSelected()
            {
                app.Tap("pickerGender");

                app.Tap("Male");

                String gender = app.Query("pickerGender")[0].Text;

                Assert.AreEqual("Male", gender);
            }

        }
    }
}
