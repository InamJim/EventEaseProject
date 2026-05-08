using EventEase.Data;
using EventEase.Models;
using EventEase.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Services
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetAll()
        {
            return _context.Bookings
                .Select(b => new Booking
                {
                    BookingId = b.BookingId,
                    BookingReferenceNumber = b.BookingReferenceNumber,
                    BookingDate = b.BookingDate,
                    BookingStatus = b.BookingStatus,
                    Notes = b.Notes,
                    EventId = b.EventId,
                    VenueId = b.VenueId
                })
                .ToList();
        }

        public Booking GetById(int id)
        {
            return _context.Bookings.Find(id);
        }
        public IEnumerable<Booking> Search(string keyword)
        {
            return _context.Bookings
                .Where(b =>
                    b.BookingReferenceNumber.Contains(keyword) ||
                    b.BookingStatus.Contains(keyword) ||
                    b.EventId.ToString().Contains(keyword) ||
                    b.VenueId.ToString().Contains(keyword)
                )
                .ToList();
        }
        public bool Create(Booking booking)
        {
            var conflict = _context.Bookings.Any(b =>
                b.VenueId == booking.VenueId &&
                b.BookingDate == booking.BookingDate
            );

            if (conflict)
            {
                return false; 
            }

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return true;
        }

        public void Delete(int id)
        {
            var booking = _context.Bookings.Find(id);

            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }
        }
    }
}