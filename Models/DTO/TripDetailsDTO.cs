namespace TripsAPI.Models.DTO
{
    public class TripDetailsDTO
    {
        public int Id { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public string? Operator { get; set; }
        public string? Status { get; set; }
        public DateTime? ScheduledStartDateTime { get; set; }
        public DateTime? ScheduledEndDateTime { get; set; }
    }
}
