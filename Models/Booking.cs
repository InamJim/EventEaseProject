namespace EventEase.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public string BookingReferenceNumber { get; set; }

        public int EventId { get; set; }
        public int VenueId { get; set; }

        public DateTime BookingDate { get; set; }

        public string BookingStatus { get; set; }
        public string Notes { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}