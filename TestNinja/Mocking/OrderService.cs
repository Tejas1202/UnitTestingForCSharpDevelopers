namespace TestNinja.Mocking
{
    public class OrderService
    {
        private IStorage _storage;

        public OrderService(IStorage storage)
        {
            _storage = storage;
        }

        public int PlaceOrder(Order order)
        {
            // Interaction testing: We wanna Assert that Store method is passed with the same object as we pass in PlaceOrder method
            var orderId = _storage.Store(order);
            //...

            return orderId;
        }
    }

    public class Order {};

    public interface IStorage
    {
        int Store(object obj);
    }

    public class Storage
    {
        public int Store(Order order)
        {
            return 0;
        }
    }
}
