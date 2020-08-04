using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.MockingTests
{
    [TestFixture]
    public class ProductTests
    {
        // Clean Test Method
        [Test]
        public void GetPrice_GoldCustomer_Apply30PercentDiscount()
        {
            var product = new Product { ListPrice = 100 };

            var result = product.GetPrice(new Customer { IsGold = true});

            Assert.That(result, Is.EqualTo(70));
        }

        // Example of Mock abuse where we creating Mock object unnecessarily as it doesn't have any external dependency
        // Looks fatty and ugly, so avoid to extract interface from every class and unit test it using Mocks
        [Test]
        public void GetPrice2_GoldCustomer_Apply30PercentDiscount()
        {
            var customer = new Mock<ICustomer>();
            customer.Setup(c => c.IsGold).Returns(true);
            var product = new Product { ListPrice = 100 };

            var result = product.GetPrice2(customer.Object);

            Assert.That(result, Is.EqualTo(70));
        }
    }
}
