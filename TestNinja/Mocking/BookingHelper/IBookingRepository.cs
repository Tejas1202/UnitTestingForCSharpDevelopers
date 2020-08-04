using System.Linq;

namespace TestNinja.Mocking.BookingHelper
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }
}