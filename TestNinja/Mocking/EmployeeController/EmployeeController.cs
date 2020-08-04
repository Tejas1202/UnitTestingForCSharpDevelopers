using System.Data.Entity;

namespace TestNinja.Mocking.EmployeeController
{
    public class EmployeeController
    {
        #region Legacy code calling the database inside class itself
        //private EmployeeContext _db;

        //public EmployeeController(EmployeeContext db)
        //{
        //    _db = db;
        //}

        //public ActionResult DeleteEmployee(int id)
        //{
        //    var employee = _db.Employees.Find(id);
        //    _db.Employees.Remove(employee);
        //    _db.SaveChanges();

        //    return RedirectToAction("Employees");
        //}
        #endregion

        #region Proper seperation of concerns, nowhere calling the database directly here and delegating that task to IEmployeeStorage object
        private IEmployeeStorage _storage;

        public EmployeeController(IEmployeeStorage storage)
        {
            _storage = storage;
        }

        public ActionResult DeleteEmployee(int id)
        {
            _storage.DeleteEmployee(id);

            return RedirectToAction("Employees");
        }
        #endregion

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { };

    public class RedirectResult : ActionResult { };

    // Bcoz we aren't doing anything here, so didn't derive from DbContext
    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges() { }
    }

    public class Employee { };
}
