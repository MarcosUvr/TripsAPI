namespace TripsAPI.Models.Domain
{
    public class Trip
    {
        public int Id { get; set; }
        public int IdOrigin { get; set; }
        public int IdDestination { get; set; }
        public int IdOperator { get; set; }
        public int IdStatus { get; set; }
        public DateTime? ScheduledStartDateTime { get; set; } = DateTime.UtcNow;
        public DateTime? ScheduledEndDateTime { get; set; } = DateTime.UtcNow;
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Location Origin { get; set; } = null!;
        public Location Destination { get; set; } = null!;
        public Operator Operator { get; set; } = null!;
        public TripStatus Status { get; set; } = null!;
    }
}
