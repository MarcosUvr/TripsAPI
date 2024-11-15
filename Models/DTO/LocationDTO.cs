namespace TripsAPI.Models.DTO
{
    public class LocationDTO
    {
        public string? Name { get; set; }
        public string? LocationType { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
    }
}
