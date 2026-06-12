namespace EventEase.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public Venue? Venue { get; set; }
        public int? VenueId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int EventTypeId { get; set; }
        public EventType? EventType { get; set; }
    }
}