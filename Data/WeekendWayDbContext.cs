using Microsoft.EntityFrameworkCore;
using T2HackathonCase2.Entities;

namespace T2HackathonCase2.Data
{
    public class WeekendWayDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DbSet<OpeningHours> OpeningHours { get; set; }

        public DbSet<UserWithLocation> UserWithLocations { get; set; }
 
        public WeekendWayDbContext(DbContextOptions<WeekendWayDbContext> options) : base(options)
        {

        }
    }
}
