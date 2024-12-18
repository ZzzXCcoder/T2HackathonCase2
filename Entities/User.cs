﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2HackathonCase2.Entities
{
    public class User
    {
        [Required]
        [Key]
        public long Id { get; set; }

        [Required]
        public long ChatId { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? Company {get; set; }
        public double? Latitude { get; set; }

        public int? Duration { get; set; }

        public double? Longtute { get; set; }
        public long? LocationId { get; set; }

        public int Currentlocation {get; set; }

    }
}
