using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class DBContextDatabase : DbContext  // Correct name here
    {
        public DBContextDatabase(DbContextOptions<DBContextDatabase> options) : base(options) { }

        // Your DbSets here
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Enclosure> Enclosures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Zoo> Zoos { get; set; }
    }
}
