using NUnit.Framework;
using prj3beer.Models;

namespace nUnitTests
{
    class PreferenceTests
    {
        // Objects
        Preference mockPref;

        // Setup the Preference object for testing
        [SetUp]
        public void Setup()
        {
            mockPref = new Preference();
            mockPref.BeverageID = 1;
            mockPref.Temperature = 2;
        }

        [Test]
        public void TestThatPreferenceHasValidPreferenceID()
        {
            mockPref.BeverageID = 3;

            var errors = ValidationHelper.Validate(mockPref);

            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatPreferenceHasValidTemperatureSetToUpperBoundary()
        {
            mockPref.Temperature = 30;

            var errors = ValidationHelper.Validate(mockPref);

            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatPreferenceHasValidTemperatureSetToLowerBoundary()
        {
            mockPref.Temperature = -30;

            var errors = ValidationHelper.Validate(mockPref);

            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatPreferenceHasInValidTemperatureSetToUpperBoundaryPlusOne()
        {
            mockPref.Temperature = 31;

            var errors = ValidationHelper.Validate(mockPref);

            Assert.IsTrue(errors.Count == 1);

            Assert.AreEqual("Target Temperature cannot be below -30C or above 30C", errors[0].ToString());
        }

        [Test]
        public void TestThatPreferenceHasInValidTemperatureSetToLowerBoundaryPlusOne()
        {
            mockPref.Temperature = -31;

            var errors = ValidationHelper.Validate(mockPref);

            Assert.IsTrue(errors.Count == 1);
            Assert.AreEqual("Target Temperature cannot be below -30C or above 30C", errors[0].ToString());
        }
    }
}