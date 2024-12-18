using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2HackathonCase2.Entities
{
    // Класс для хранения информации о часах работы
    public class OpeningHours
    {
        [Key]
        public Guid Id { get; set; }  // Основной ключ для записи часов работы

        public string Start { get; set; } // День недели
        public string Duration { get; set; } // Время открытия
        public string Recurence { get; set; } // Время закрытия

    }

    // Основной класс для локации
    public class Location
    {
        [Required]
        [Key]
        public string Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public string? Category { get; set; }

        public string? ImageURL { get;set ; }

    }
}
