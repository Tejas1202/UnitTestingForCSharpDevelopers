using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.MockingTests
{
    [TestFixture]
    class OrderServiceTests
    {
        [Test]
        public void PlaceOrder_WhenCalled_StoreTheOrder()
        {
            var storage = new Mock<IStorage>();
            var orderService = new OrderService(storage.Object);

            var order = new Order();
            orderService.PlaceOrder(order);

            // Assert that storage.Store is called
            // Verify Method: to verify that the given method is called with the right arguments or not
            storage.Verify(s => s.Store(order));
        }
    }
}
