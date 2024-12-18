using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2HackathonCase2.Entities
{
    public class UserWithLocation
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]

        public long UserId { get; set; }

        [ForeignKey("Location")]
        public string LocationId { get; set; }
        [ForeignKey("OpeningHours")]
        public Guid OpeningHoursId { get; set; }

    }
}
