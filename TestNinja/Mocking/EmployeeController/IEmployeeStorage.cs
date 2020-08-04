namespace TestNinja.Mocking.EmployeeController
{
    // Hence now our controller doesn't care about the implementation detail whether's it's deleting the data using EF or plain Sql
    // like executing queries. All it cares about is there should be a method for Deleting employee
    public interface IEmployeeStorage
    {
        void DeleteEmployee(int id);
    }
}