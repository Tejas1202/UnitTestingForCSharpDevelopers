using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking.BookingHelper
{
    public static class BookingHelper
    {
        #region Legacy code
        //public static string OverlappingBookingsExist(Booking booking)
        //{
        //    if (booking.Status == "Cancelled")
        //        return string.Empty;

        //    var unitOfWork = new UnitOfWork();
        //    var bookings =
        //        unitOfWork.Query<Booking>()
        //            .Where(
        //                b => b.Id != booking.Id && b.Status != "Cancelled");

        //    var overlappingBooking =
        //        bookings.FirstOrDefault(
        //            b =>
        //                booking.ArrivalDate >= b.ArrivalDate
        //                && booking.ArrivalDate < b.DepartureDate
        //                || booking.DepartureDate > b.ArrivalDate
        //                && booking.DepartureDate <= b.DepartureDate);

        //    return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        //}
        #endregion

        // As this class and method is static, we cannot use ctor/property injection, so can change this static to instance method but it may have implications
        // So we're gonna use Parameter injection here assuming our DI framework does support that
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository bookingRepository)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;

            var bookings = bookingRepository.GetActiveBookings(booking.Id);

            var overlappingBooking =
                bookings.FirstOrDefault(
                    b =>
                        booking.ArrivalDate < b.DepartureDate
                        && b.ArrivalDate < booking.DepartureDate);
            // Above lambda expression was changed after it failed our third unit test (see legacy code for reference)
            // And hence, we can detect bugs in our production code early using Unit Testing

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public class UnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}
