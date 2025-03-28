using Bogus;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, DBContextDatabase context)
        {
            // Check if the database has been seeded
            if (context.Categories.Any() || context.Enclosures.Any() || context.Animals.Any())
            {
                return; // DB has been seeded
            }

            // Seed Categories using Bogus
            var categoryNames = new[] { "Mammals", "Birds", "Reptiles", "Amphibians", "Fish" };
            var categories = categoryNames.Select(name => new Category { Name = name }).ToList();
            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Seed Enclosures using Bogus
            var enclosureFaker = new Faker<Enclosure>()
                .RuleFor(e => e.Name, f => f.Lorem.Word())
                .RuleFor(e => e.Climate, f => f.PickRandom<ClimateEnum>())
                .RuleFor(e => e.HabitatType, f => f.PickRandom<flagsEnum>())
                .RuleFor(e => e.SecurityLevel, f => f.PickRandom<SecurityLevelEnum>())
                .RuleFor(e => e.Size, f => f.Random.Double(100, 1000));

            var enclosures = enclosureFaker.Generate(5);
            context.Enclosures.AddRange(enclosures);
            context.SaveChanges();

            // Seed Animals using Bogus
            var animalFaker = new Faker<Animal>()
                .RuleFor(a => a.Name, f => f.Name.FirstName())
                .RuleFor(a => a.Species, f => f.PickRandom(new[] { "Lion", "Eagle", "Turtle", "Penguin", "Snake" }))
                .RuleFor(a => a.Category, f => f.PickRandom(categories))
                .RuleFor(a => a.Enclosure, f => f.PickRandom(enclosures))
                .RuleFor(a => a.Size, f => f.PickRandom<SizeEnum>())
                .RuleFor(a => a.DietaryClass, f => f.PickRandom<DietaryEnum>())
                .RuleFor(a => a.ActivityPattern, f => f.PickRandom<ActivityPatternEnum>())
                .RuleFor(a => a.Prey, f => f.Random.Bool())
                .RuleFor(a => a.SpaceRequirement, f => f.Random.Double(10, 200))
                .RuleFor(a => a.SecurityRequirement, f => f.PickRandom<SecurityLevelEnum>())
                .RuleFor(a => a.SpaceRequirement, f => f.Random.Int(10, 200))
                .RuleFor(a => a.FeedingTime, f => f.Date.Between(DateTime.Now.AddHours(-12), DateTime.Now.AddHours(12)).TimeOfDay);

            var animals = animalFaker.Generate(20);
            context.Animals.AddRange(animals);
            context.SaveChanges();
        }
    }
}