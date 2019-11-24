using NUnit.Framework;
using prj3beer.Models;
using prj3beer.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests 
    {


        [SetUp]
        public void Setup()
        {
            Brand newBrand = new Brand();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void TestThatBrandIsAddedToList()
        {


        }
    }
}