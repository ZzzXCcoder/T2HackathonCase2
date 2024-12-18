using System.ComponentModel.DataAnnotations;

namespace T2HackathonCase2.Dtos
{
    public class LocationDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Category { get; set; }

        public string ImageURL { get; set; }
    }
}
