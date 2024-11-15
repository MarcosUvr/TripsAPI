namespace TripsAPI.Models.DTO
{
    public class PostTripDTO
    {
        public int IdOrigin { get; set; }
        public int IdDestination { get; set; }
        public int IdOperator { get; set; }
        public DateTime? ScheduledStartDateTime { get; set; }
        public DateTime? ScheduledEndDateTime { get; set; }
    }
}
