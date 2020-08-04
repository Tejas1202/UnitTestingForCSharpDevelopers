namespace TestNinja.Fundamentals
{
    public class Reservation
    {
        public User MadeBy { get; set; }

        // We've three scenarios here or three execution paths
        // So there are three ways to get out of this method
        public bool CanBeCancelledBy(User user)
        {
            return (user.IsAdmin || MadeBy == user);

            // So one of the major benefits of Unit Testing is refactoring the code doesn't break Test Cases
            // and hence you can be more confident about your code change
            //if (user.IsAdmin)
            //    return true;

            //if (MadeBy == user)
            //    return true;

            //return false;
        }
    }

    public class User
    {
        public bool IsAdmin { get; set; }
    }
}
