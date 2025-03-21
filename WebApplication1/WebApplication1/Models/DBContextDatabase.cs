using Bogus;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class DBContextDatabase : DbContext
    {
        public DBContextDatabase(DbContextOptions<DBContextDatabase> options) : base(options) { }

        // DbSets
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Enclosure> Enclosures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Zoo> Zoos { get; set; }

        // Seed data method
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            var categoryFaker = new Faker<Category>()
                .RuleFor(c => c.Id, f => f.IndexFaker + 1) // Generate unique Ids
                .RuleFor(c => c.Name, f => f.PickRandom(new[] { "Mammal", "Bird", "Reptile", "Fish", "Amphibian" }));

            var categories = categoryFaker.Generate(5);
            modelBuilder.Entity<Category>().HasData(categories);

            // Seed Enclosures
            var enclosureFaker = new Faker<Enclosure>()
                .RuleFor(e => e.Id, f => f.IndexFaker + 1) // Generate unique Ids
                .RuleFor(e => e.Name, f => f.Lorem.Word())
                .RuleFor(e => e.Size, f => f.Random.Number(100, 1000))
                .RuleFor(e => e.Climate, f => f.PickRandom<ClimateEnum>())
                .RuleFor(e => e.HabitatType, f => f.PickRandom<flagsEnum>())
                .RuleFor(e => e.SecurityLevel, f => f.PickRandom<SecurityLevelEnum>());

            var enclosures = enclosureFaker.Generate(5);
            modelBuilder.Entity<Enclosure>().HasData(enclosures);

            // Seed Animals
            var animalId = 1;
            var animalFaker = new Faker<Animal>()
                .RuleFor(a => a.Id, f => animalId++) // Ensure unique Ids with positive values
                .RuleFor(a => a.Name, f => f.Commerce.ProductName())
                .RuleFor(a => a.Species, f => f.PickRandom<speciesEnum>().ToString())
                .RuleFor(a => a.Size, f => f.PickRandom<SizeEnum>())
                .RuleFor(a => a.DietaryClass, f => f.PickRandom<DietaryEnum>())
                .RuleFor(a => a.ActivityPattern, f => f.PickRandom<ActivityPatternEnum>())
                .RuleFor(a => a.Prey, f => f.Random.Bool())
                .RuleFor(a => a.SpaceRequirement, f => f.Random.Number(100, 1000))
                .RuleFor(a => a.SecurityRequirement, f => f.PickRandom<SecurityLevelEnum>())
                .RuleFor(a => a.CategoryId, f => f.PickRandom(categories).Id); // Link to existing category

            var animals = animalFaker.Generate(10);
            modelBuilder.Entity<Animal>().HasData(animals);

            // Seed Zoos
            var zooId = 1;
            var zooFaker = new Faker<Zoo>()
                .RuleFor(z => z.Id, f => zooId++) // Ensure unique Ids with positive values
                .RuleFor(z => z.Name, f => f.Company.CompanyName());

            var zoos = zooFaker.Generate(2);
            modelBuilder.Entity<Zoo>().HasData(zoos);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Id property for the Animal entity
            modelBuilder.Entity<Animal>()
                .Property(a => a.Id)
                .ValueGeneratedNever(); // Disable value generation for seed data

            base.OnModelCreating(modelBuilder);
        }
    }
}
