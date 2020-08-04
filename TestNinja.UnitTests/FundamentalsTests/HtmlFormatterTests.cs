using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.FundamentalsTests
{
    // Testing method returning Strings
    [TestFixture]
    class HtmlFormatterTests
    {
        // Note: You should not handle all the scenarios ranging from general to specific (can do if there's any exception)
        // But here, you should write only the Assert statements acc to your requirement i.e. either general/specific (this is just for demo)
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
        {
            var formatter = new HtmlFormatter();

            var result = formatter.FormatAsBold("abc");

            // Specific
            Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);
            // hence can also append IgnoreCase as EqualTo is by default Case Sensitive

            // More general
            Assert.That(result, Does.StartWith("<strong>"));
            Assert.That(result, Does.EndWith("</strong>"));
            Assert.That(result, Does.Contain("abc"));
        }
    }
}
