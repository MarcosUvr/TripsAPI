namespace TripsAPI.Models.Domain
{
    public class Location
    {
        public int Id { get; set; }
        public string? Name { get; set; } = "";
        public string? LocationType { get; set; } = "";
        public string? Country { get; set; } = "";
        public string? State { get; set; } = "";
        public float? Latitude { get; set; } = 0;
        public float? Longitude { get; set; } = 0;
    }
}
