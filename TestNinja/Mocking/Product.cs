namespace TestNinja.Mocking
{
    // Example of Mock Abuse
    public class Product
    {
        public int ListPrice { get; set; }

        // Right way
        public float GetPrice(Customer customer)
        {
            if (customer.IsGold)
                return ListPrice * 0.7f;

            return ListPrice;
        }

        // Not ideal
        public float GetPrice2(ICustomer customer)
        {
            if (customer.IsGold)
                return ListPrice * 0.7f;

            return ListPrice;
        }
    }

    // Now if we extract an interface just to pass this to GetPrice method, then our Test Methods gets fatty 
    // as these Mock objects are unnecessary and they aren't talking to any external resource
    public interface ICustomer
    {
        bool IsGold { get; set; }
    }

    public class Customer : ICustomer
    {
        public bool IsGold { get; set; }
    }
}
