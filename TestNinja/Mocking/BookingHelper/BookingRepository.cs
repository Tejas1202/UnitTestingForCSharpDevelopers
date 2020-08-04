using System.Linq;

namespace TestNinja.Mocking.BookingHelper
{
    class BookingRepository : IBookingRepository
    {
        // Keeping this as on optional parameter as we may want to call this method somewhere else where we just want active bookings
        // Note: Make sure you don't break anything as a part of this refactoring as we're changing logic a bit
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings =
                unitOfWork.Query<Booking>()
                    .Where(
                        b => b.Status != "Cancelled");

            if (excludedBookingId.HasValue)
                bookings = bookings.Where(b => b.Id != excludedBookingId.Value);

            return bookings;
        }
    }
}
