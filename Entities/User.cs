using System.ComponentModel.DataAnnotations;

namespace T2HackathonCase2.Entities
{
    public class User
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public long TelegramId { get; set; }

        string? UserName { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public string? Description {  get; set; }
    }
}
