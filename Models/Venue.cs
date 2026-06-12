namespace EventEase.Models
{
    public class Venue
    {
        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool Availability { get; set; } = true;
        public ICollection<Event> Events { get; set; }
        = new List<Event>();
    }
}