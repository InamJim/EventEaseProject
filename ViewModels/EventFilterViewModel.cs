using EventEase.Models;

namespace EventEase.ViewModels
{
    public class EventFilterViewModel
    {
        public List<Event> Events { get; set; }
            = new();

        public List<EventType> EventTypes { get; set; }
            = new();

        public int? EventTypeId { get; set; }

        public bool? Availability { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}