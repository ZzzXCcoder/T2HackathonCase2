using System.ComponentModel.DataAnnotations;
using T2HackathonCase2.Entities;

public class Location
{
    [Required]
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public double Latitude { get; set; }

    [Required]
    public double Longitude { get; set; }

}
