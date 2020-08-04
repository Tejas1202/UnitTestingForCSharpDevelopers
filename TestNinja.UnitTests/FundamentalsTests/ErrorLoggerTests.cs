using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.FundamentalsTests
{
    // Testing methods that are void/throws exception/raises events
    [TestFixture]
    public class ErrorLoggerTests
    {
        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            var logger = new ErrorLogger();

            logger.Log("a");

            // Hence checking whether object's state is properly changed or not
            Assert.That(logger.LastError, Is.EqualTo("a"));
        }

        // Tests when method throws an exception
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string error)
        {
            var logger = new ErrorLogger();

            // Directly calling the method like this will throw an exception and our test case will fail
            //logger.Log(error);

            // Hence assigning this to a delegate through lambda expression
            Assert.That(() => logger.Log(error), Throws.ArgumentNullException);
        }

        // Test when method raises an event
        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            var logger = new ErrorLogger();
            var id = Guid.Empty;
            // Subscribing to the event, so when it's raised, args will have the value sent by the event
            logger.ErrorLogged += (sender, args) => { id = args; };

            logger.Log("a");

            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
