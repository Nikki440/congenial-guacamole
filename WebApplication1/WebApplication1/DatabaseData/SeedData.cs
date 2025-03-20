using Bogus;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, DBContextDatabase context)
        {
            // Check if data already exists in the tables
            if (context.Animals.Any() || context.Categories.Any() || context.Enclosures.Any())
            {
                return;   // If there are already records, do not seed
            }

            // Seed Categories
            var categories = new List<Category>
            {
                new Category { Name = "Mammals" },
                new Category { Name = "Birds" },
                new Category { Name = "Reptiles" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Seed Enclosures
            var enclosures = new List<Enclosure>
            {
                new Enclosure { Name = "Savannah", Climate = ClimateEnum.Tropical, HabitatType = flagsEnum.Forest | flagsEnum.Grassland, SecurityLevel = SecurityLevelEnum.Medium, Size = 500 },
                new Enclosure { Name = "Aviary", Climate = ClimateEnum.Temperate, HabitatType = flagsEnum.Forest | flagsEnum.Aquatic, SecurityLevel = SecurityLevelEnum.Low, Size = 300 }
            };

            context.Enclosures.AddRange(enclosures);
            context.SaveChanges();

            // Seed Animals using Bogus
            var faker = new Faker();

            var animals = new List<Animal>
            {
                new Animal
                {
                    Name = faker.Name.FirstName(),
                    Species = "Lion",
                    Category = categories[0], // Mammals
                    Enclosure = enclosures[0], // Savannah
                    Size = SizeEnum.Large,
                    DietaryClass = DietaryEnum.Carnovore,
                    ActivityPattern = ActivityPatternEnum.Diurnal,
                    prey = true,
                    SpaceRequirement = 100,
                    SecurityRequirement = SecurityLevelEnum.High
                },
                new Animal
                {
                    Name = faker.Name.FirstName(),
                    Species = "Eagle",
                    Category = categories[1], // Birds
                    Enclosure = enclosures[1], // Aviary
                    Size = SizeEnum.Medium,
                    DietaryClass = DietaryEnum.Carnovore,
                    ActivityPattern = ActivityPatternEnum.Diurnal,
                    prey = true,
                    SpaceRequirement = 50,
                    SecurityRequirement = SecurityLevelEnum.Low
                },
                new Animal
                {
                    Name = faker.Name.FirstName(),
                    Species = "Turtle",
                    Category = categories[2], // Reptiles
                    Enclosure = enclosures[0], // Savannah
                    Size = SizeEnum.Small,
                    DietaryClass = DietaryEnum.Herbivore,
                    ActivityPattern = ActivityPatternEnum.Nocturnal,
                    prey = false,
                    SpaceRequirement = 25,
                    SecurityRequirement = SecurityLevelEnum.Medium
                }
            };

            context.Animals.AddRange(animals);
            context.SaveChanges();
        }
    }
}
