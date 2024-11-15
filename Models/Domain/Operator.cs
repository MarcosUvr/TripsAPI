﻿namespace TripsAPI.Models.Domain
{
    public class Operator
    {
        public int Id { get; set; }
        public string? Name { get; set; } = "";
        public string? LastName { get; set; } = "";
        public string? Phone { get; set; } = "";
        public string? Email { get; set; } = "";
        public bool? IsActive { get; set; } = true;
    }
}
