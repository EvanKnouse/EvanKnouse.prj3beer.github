using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using prj3beer.Models;
using System.ComponentModel.DataAnnotations;

namespace nUnitTests
{
    class PreferenceTests
    {
        // Objects
        Beverage mockBev;
        Preference preference;

        // Setup the Beverage object for testing
        [SetUp]
        public void Setup()
        {
            mockBev = new Beverage(1, "MGD", "Miller", 2);
        }

        // Test that the validation process returns no errors for a properly created Preference object
        [Test]
        public void TestPreferenceIsCreatedSuccessfully()
        {
            preference = new Preference(1, mockBev, mockBev.IdealTemp);

            var errors = ValidationHelper.Validate(preference);

            Assert.IsTrue(errors[0] == null);
        }

        // Test that validation returns an error for the preference ID attribute when it is not set
        [Test]
        public void TestPreferenceHasInvalidID()
        {
            preference = new Preference();

            preference.PrefBev = mockBev;
            preference.FaveTemp = mockBev.IdealTemp;

            var errors = ValidationHelper.Validate(preference);

            Assert.AreEqual("ID is required", errors[0].ErrorMessage);
        }

        // Test that validation returns an error for the preferred beverage attribute when it is not set
        [Test]
        public void TestPreferenceHasInvalidBeverage()
        {
            preference = new Preference(1, null, mockBev.IdealTemp);

            var errors = ValidationHelper.Validate(preference);

            Assert.AreEqual("Beverage object is required", errors[0].ErrorMessage);
        }

        // Test that validation returns an error for the favourite temperature attribute when it is set to an invalid high temperature
        [Test]
        public void TestPreferenceHasInvalidHighFaveTemp()
        {
            double dInvalidTemp = 31;

            preference = new Preference(1, mockBev, dInvalidTemp);

            var errors = ValidationHelper.Validate(preference);

            Assert.AreEqual("Target Temperature cannot be below -30C or above 30C", errors[0].ErrorMessage);
        }

        // Test that validation returns an error for the favourite temperature attribute when it is set to an invalid high temperature
        [Test]
        public void TestPreferenceHasInvalidLowFaveTemp()
        {
            double dInvalidTemp = -31;

            preference = new Preference(1, mockBev, dInvalidTemp);

            var errors = ValidationHelper.Validate(preference);

            Assert.AreEqual("Target Temperature cannot be below -30C or above 30C", errors[0].ErrorMessage);
        }
    }
}