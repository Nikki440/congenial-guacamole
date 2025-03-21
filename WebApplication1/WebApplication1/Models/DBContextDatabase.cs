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
        public static void SeedData(DBContextDatabase context)
        {
            // Seed Categories
            var categoryFaker = new Faker<Category>()
                .RuleFor(c => c.CategoryId, f => f.IndexFaker + 1) // Generate unique Ids
                .RuleFor(c => c.Name, f => f.PickRandom(new[] { "Mammal", "Bird", "Reptile", "Fish", "Amphibian" }));

            var categories = categoryFaker.Generate(5);
            foreach (var category in categories)
            {
                var existingCategory = context.Categories.Find(category.CategoryId);
                if (existingCategory == null)
                {
                    context.Categories.Add(category);
                }
                else
                {
                    context.Entry(existingCategory).CurrentValues.SetValues(category);
                }
            }

            // Seed Enclosures
            var enclosureFaker = new Faker<Enclosure>()
                .RuleFor(e => e.EnclosureId, f => f.IndexFaker + 1) // Generate unique Ids
                .RuleFor(e => e.Name, f => f.Lorem.Word())
                .RuleFor(e => e.Size, f => f.Random.Number(100, 1000))
                .RuleFor(e => e.Climate, f => f.PickRandom<ClimateEnum>())
                .RuleFor(e => e.HabitatType, f => f.PickRandom<flagsEnum>())
                .RuleFor(e => e.SecurityLevel, f => f.PickRandom<SecurityLevelEnum>());

            var enclosures = enclosureFaker.Generate(5);
            foreach (var enclosure in enclosures)
            {
                var existingEnclosure = context.Enclosures.Find(enclosure.EnclosureId);
                if (existingEnclosure == null)
                {
                    context.Enclosures.Add(enclosure);
                }
                else
                {
                    context.Entry(existingEnclosure).CurrentValues.SetValues(enclosure);
                }
            }

            // Seed Animals
            var animalId = 1;
            var animalFaker = new Faker<Animal>()
                .RuleFor(a => a.AnimalId, f => animalId++) // Ensure unique Ids with positive values
                .RuleFor(a => a.Name, f => f.Commerce.ProductName())
                .RuleFor(a => a.Species, f => f.PickRandom<speciesEnum>().ToString())
                .RuleFor(a => a.Size, f => f.PickRandom<SizeEnum>())
                .RuleFor(a => a.DietaryClass, f => f.PickRandom<DietaryEnum>())
                .RuleFor(a => a.ActivityPattern, f => f.PickRandom<ActivityPatternEnum>())
                .RuleFor(a => a.Prey, f => f.Random.Bool())
                .RuleFor(a => a.SpaceRequirement, f => f.Random.Number(100, 1000))
                .RuleFor(a => a.SecurityRequirement, f => f.PickRandom<SecurityLevelEnum>())
                .RuleFor(a => a.CategoryId, f => f.PickRandom(context.Categories.ToList()).CategoryId) // Link to existing category
                .RuleFor(a => a.EnclosureId, f => f.PickRandom(context.Enclosures.ToList()).EnclosureId); // Link to existing enclosure

            var animals = animalFaker.Generate(10);
            foreach (var animal in animals)
            {
                var existingAnimal = context.Animals.Find(animal.AnimalId);
                if (existingAnimal == null)
                {
                    context.Animals.Add(animal);
                }
                else
                {
                    context.Entry(existingAnimal).CurrentValues.SetValues(animal);
                }
            }

            // Seed Zoos
            var zooId = 1;
            var zooFaker = new Faker<Zoo>()
                .RuleFor(z => z.Id, f => zooId++) // Ensure unique Ids with positive values
                .RuleFor(z => z.Name, f => f.Company.CompanyName());

            var zoos = zooFaker.Generate(2);
            foreach (var zoo in zoos)
            {
                var existingZoo = context.Zoos.Find(zoo.Id);
                if (existingZoo == null)
                {
                    context.Zoos.Add(zoo);
                }
                else
                {
                    context.Entry(existingZoo).CurrentValues.SetValues(zoo);
                }
            }

            context.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Id property for the Animal entity
            modelBuilder.Entity<Animal>()
                .Property(a => a.AnimalId)
                .ValueGeneratedNever(); // Disable value generation for seed data

            // Configure the relationships
            modelBuilder.Entity<Animal>()
                .HasOne(a => a.Enclosure)
                .WithMany(e => e.Animals)
                .HasForeignKey(a => a.EnclosureId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
