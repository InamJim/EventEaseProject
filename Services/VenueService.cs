using EventEase.Data;
using EventEase.Models;
using EventEase.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Services
{
    public class VenueService : IVenueService
    {
        private readonly AppDbContext _context;

        public VenueService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Venue> GetAll()
        {
            return _context.Venues.ToList();
        }

        public Venue GetById(int id)
        {
            return _context.Venues.Find(id);
        }

        public IEnumerable<Venue> Search(string keyword)
        {
            return _context.Venues
                .Where(v =>
                    v.VenueName.Contains(keyword) ||
                    v.Location.Contains(keyword)
                )
                .ToList();
        }
        public void Create(Venue venue)
        {
            _context.Venues.Add(venue);
            _context.SaveChanges();
        }

        public void Update(Venue venue)
        {
            _context.Venues.Update(venue);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var venue = _context.Venues.Find(id);

            if (venue == null)
                return;

            bool hasBookings = _context.Bookings
                .Any(b => b.VenueId == id);

            if (hasBookings)
            {
                throw new Exception(
                    "Cannot delete venue because it is linked to booking."
                );
            }

            _context.Venues.Remove(venue);
            _context.SaveChanges();
        }
    }
}