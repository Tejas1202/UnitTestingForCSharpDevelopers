using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.FundamentalsTests
{
    [TestFixture]
    public class DateHelperTests
    {
        [Test]
        [TestCase("2020-01-01", "2020-02-01")]
        [TestCase("2020-12-01", "2021-01-01")]
        public void FirstOfNextMonth_WhenCalled_ReturnFirstOfNextMonth(DateTime date, DateTime expectedResult)
        {
            var result = DateHelper.FirstOfNextMonth(date);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
