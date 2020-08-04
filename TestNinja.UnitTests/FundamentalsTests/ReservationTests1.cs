using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.FundamentalsTests
{
    // Using NUnit Test Framework
    // Install the following packages from PMC
    // 1. install-package NUnit -Version 3.8
    // 2. Install-Package NUnit3TestAdapter -Version 3.8.0 => Needed to run NUnit Tests inside VS as by default VS runs only MSTests
    [TestFixture]
    public class ReservationTests1
    {
        // Naming: MethodName_Scenario_ExpectedBehaviour
        [Test]
        public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
        {
            var user = new User();
            var reservation = new Reservation { MadeBy = user };

            var result = reservation.CanBeCancelledBy(user);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
        {
            var reservation = new Reservation { MadeBy = new User() };

            var result = reservation.CanBeCancelledBy(new User());

            Assert.That(result, Is.False);
        }
    }
}
