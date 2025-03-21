﻿using Bogus;
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
                .RuleFor(c => c.Name, f => f.PickRandom(new[] { "Mammal", "Bird", "Reptile", "Fish", "Amphibian" }));

            var categories = categoryFaker.Generate(5); // Generate 5 categories
            modelBuilder.Entity<Category>().HasData(categories);

            // Seed Enclosures
            var enclosureFaker = new Faker<Enclosure>()
                .RuleFor(e => e.Name, f => f.Lorem.Word())
                .RuleFor(e => e.Size, f => f.Random.Number(100, 1000))
                .RuleFor(e => e.Climate, f => f.PickRandom<ClimateEnum>())
                .RuleFor(e => e.HabitatType, f => f.PickRandom<flagsEnum>())
                .RuleFor(e => e.SecurityLevel, f => f.PickRandom<SecurityLevelEnum>());

            var enclosures = enclosureFaker.Generate(5); // Generate 5 enclosures
            modelBuilder.Entity<Enclosure>().HasData(enclosures);

            // Seed Animals
            var animalFaker = new Faker<Animal>()
                .RuleFor(a => a.Name, f => f.Commerce.ProductName()) // Generates a product-like name for the animal
                .RuleFor(a => a.Species, f => f.PickRandom<speciesEnum>().ToString())
                .RuleFor(a => a.Size, f => f.PickRandom<SizeEnum>())
                .RuleFor(a => a.DietaryClass, f => f.PickRandom<DietaryEnum>())
                .RuleFor(a => a.ActivityPattern, f => f.PickRandom<ActivityPatternEnum>())
                .RuleFor(a => a.prey, f => f.Random.Bool())
                .RuleFor(a => a.SpaceRequirement, f => f.Random.Number(100, 1000))
                .RuleFor(a => a.SecurityRequirement, f => f.PickRandom<SecurityLevelEnum>())
                .RuleFor(a => a.CategoryId, f => f.PickRandom(categories).Id) // Pick random category
                .RuleFor(a => a.Enclosure, f => f.PickRandom(enclosures)); // Pick random enclosure

            var animals = animalFaker.Generate(10); // Generate 10 animals
            modelBuilder.Entity<Animal>().HasData(animals);

            // Seed Zoos
            var zooFaker = new Faker<Zoo>()
                .RuleFor(z => z.Name, f => f.Company.CompanyName())
                .RuleFor(z => z.Enclosures, f => enclosures.Take(2).ToList()) // Pick first two enclosures
                .RuleFor(z => z.Animals, f => animals.Take(5).ToList()); // Pick first five animals

            var zoos = zooFaker.Generate(2); // Generate 2 zoos
            modelBuilder.Entity<Zoo>().HasData(zoos);
        }

        // Overriding OnModelCreating to add seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Call the SeedData method
            SeedData(modelBuilder);
        }
    }
}
