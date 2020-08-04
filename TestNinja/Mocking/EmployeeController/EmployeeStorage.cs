namespace TestNinja.Mocking.EmployeeController
{
    // Note: We don't need to Unit test because here we're dealing directly with the external resource
    // So, the proper way to test this method is using an integration test and ensure employee is deleted from the database
    class EmployeeStorage : IEmployeeStorage
    {
        private EmployeeContext _db;

        public EmployeeStorage(EmployeeContext db)
        {
            _db = db;
        }

        public void DeleteEmployee(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee == null)
                return;

            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }
    }
}