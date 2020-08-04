using System;

namespace TestNinja.Fundamentals
{
    public class ErrorLogger
    {
        public string LastError { get; set; }

        public event EventHandler<Guid> ErrorLogged;

        public void Log(string error)
        {
            if (string.IsNullOrWhiteSpace(error))
                throw new ArgumentNullException();

            // Void method changing state of the object by changing it's property
            LastError = error;

            // Write log to a repository/storage
            // ...

            //ErrorLogged?.Invoke(this, Guid.NewGuid());
            OnErrorLogged();
        }

        // This method is about implementation detail and hence not making it public and writing test cases on it
        // Hence, tomorrow even if we raise the event directly from the method and remove this method/pass a parameter to this method, nothing will break
        protected virtual void OnErrorLogged()
        {
            ErrorLogged?.Invoke(this, Guid.NewGuid());
        }

    }
}
