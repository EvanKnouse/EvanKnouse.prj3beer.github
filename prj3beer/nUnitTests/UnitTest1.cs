using NUnit.Framework;
using prj3beer.Models;

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
    }
}