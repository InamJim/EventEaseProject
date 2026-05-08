using EventEase.Data;
using EventEase.Models;
using EventEase.Services.Interfaces;

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
            return _context.Events.ToList();
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