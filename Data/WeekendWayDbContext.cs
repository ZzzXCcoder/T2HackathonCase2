using Microsoft.EntityFrameworkCore;
using T2HackathonCase2.Entities;

namespace T2HackathonCase2.Data
{
    public class WeekendWayDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
