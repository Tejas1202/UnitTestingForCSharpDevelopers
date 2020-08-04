using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.FundamentalsTests
{
    [TestFixture]
    public class PhoneNumberTests
    {
        private readonly string _validPhoneNumber = "1234567890";

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("123456789")]
        [TestCase("12345678901")]
        public void Parse_InvalidArg_ThrowArgumentException(string number)
        {
            Assert.That(() => PhoneNumber.Parse(number), Throws.ArgumentException);
        }

        [Test]
        public void Parse_ValidArg_SetAllThePropertiesCorrectly()
        {
            var phoneNumber = PhoneNumber.Parse(_validPhoneNumber);

            Assert.That(phoneNumber.Area, Is.EqualTo("123"));
            Assert.That(phoneNumber.Major, Is.EqualTo("456"));
            Assert.That(phoneNumber.Minor, Is.EqualTo("7890"));
        }

        [Test]
        public void ToString_WhenCalled_ShouldReturnStringWithContainingAreaMajorAndMinorInSpecifiedFormat()
        {
            var phoneNumber = PhoneNumber.Parse(_validPhoneNumber);

            var result = phoneNumber.ToString();

            Assert.That(result, Is.EqualTo($"({phoneNumber.Area}){phoneNumber.Major}-{phoneNumber.Minor}"));
        }
    }
}
