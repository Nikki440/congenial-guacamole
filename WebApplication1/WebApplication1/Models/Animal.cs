namespace WebApplication1.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public SizeEnum Size { get; set; }
        public DietaryEnum DietaryClass { get; set; }
        public ActivityPatternEnum ActivityPattern { get; set; }
        public bool Prey { get; set; }
        public int SpaceRequirement { get; set; }
        public SecurityLevelEnum SecurityRequirement { get; set; }
        public int? EnclosureId { get; set; } // Foreign key for Enclosure
        public Enclosure? Enclosure { get; set; } // Navigation property for Enclosure
        public TimeSpan? FeedingTime { get; set; } // FeedingTime as TimeSpan
    }

    public enum DietaryEnum
    {
        Carnivore,
        Herbivore,
        Omnivore,
        Insectivore,
        Piscivore
    }

    public enum speciesEnum
    {
        Fish,
        Mammels,
        Birds,
        Reptiles,
        Amphibians
    }

    public enum SizeEnum
    {
        Microscopic,
        VerySmall,
        Small,
        Medium,
        Large,
        VeryLarge
    }
    public enum ActivityPatternEnum
    {
        Diurnal,
        Nocturnal,
        Cathemeral
    }
    public enum SecurityLevelEnum
    {
        Low,
        Medium,
        High
    }
}