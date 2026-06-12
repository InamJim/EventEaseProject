using EventEase.Data;
using EventEase.Models;
using EventEase.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Event> GetAll()
        {
            return _context.Events
                .Include(e => e.EventType)
                .Include(e => e.Venue)
                .ToList();
        }

        public Event GetById(int id)
        {
            return _context.Events.Find(id);
        }

        public void Create(Event ev)
        {
            _context.Events.Add(ev);
            _context.SaveChanges();
        }

        public IEnumerable<Event> Search (
    int? eventTypeId,
    bool? availability,
    DateTime? startDate,
    DateTime? endDate )
        {
            var query = _context.Events
                .Include(e => e.EventType)
                .Include(e => e.Venue)
                .AsQueryable();

            if ( eventTypeId.HasValue )
            {
                query = query.Where(e =>
                    e.EventTypeId == eventTypeId);
            }

            if ( availability.HasValue )
            {
                query = query.Where(e =>
                    e.Venue.Availability ==
                    availability.Value);
            }

            if ( startDate.HasValue )
            {
                query = query.Where(e =>
                    e.EventDate >= startDate.Value);
            }

            if ( endDate.HasValue )
            {
                query = query.Where(e =>
                    e.EventDate <= endDate.Value);
            }

            return query.ToList();
        }

        public void Update(Event ev)
        {
            _context.Events.Update(ev);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var ev = _context.Events.Find(id);

            if (ev == null)
                return;

            bool hasBookings = _context.Bookings
                .Any(b => b.EventId == id);

            if (hasBookings)
            {
                throw new Exception(
                    "Cannot delete event because it is linked to booking."
                );
            }

            _context.Events.Remove(ev);
            _context.SaveChanges();
        }
    }
}