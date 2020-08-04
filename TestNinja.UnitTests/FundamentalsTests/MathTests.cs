using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.FundamentalsTests
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;

        // Will be called before every test is run, hence Arrange duplication can be reduced,
        // hence no need to create seperate Math instances inside every method
        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        // Scenario name kept as Generic (WhenCalled) because there's only one scenario i.e. execution path in Add method
        [Test]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Max_FirstArgumentIsGreater_ReturnTheFirstArgument()
        {
            var result = _math.Max(2, 1);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        [Ignore("Ignored to test the ignore attribute")]
        public void Max_SecondArgumentIsGreater_ReturnTheSecondArgument()
        {
            var result = _math.Max(1, 2);

            Assert.That(result, Is.EqualTo(2));
        }

        // Black-box testing, not relying on just the implementation of code and handling all scenarios considering it as a black-box
        [Test]
        public void Max_ArgumentsAreEqual_ReturnTheSameArgument()
        {
            var result = _math.Max(1, 1);

            Assert.That(result, Is.EqualTo(1));
        }

        // Parameterized Tests
        // Hence all the above tests can be reduced to this
        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        // Testing a method which returns IEnumerable
        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);

            // Too general
            //Assert.That(result, Is.Not.Empty);

            // Somewhere between general and specific
            //Assert.That(result.Count, Is.EqualTo(3));

            // A little more specific
            //Assert.That(result, Does.Contain(1));
            //Assert.That(result, Does.Contain(1));
            //Assert.That(result, Does.Contain(1));

            // A more better way to write the above statements
            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));

            // If we expect our Enumberable to be Sorted or have unique values
            //Assert.That(result, Is.Ordered);
            //Assert.That(result, Is.Unique);
        }
    }
}
