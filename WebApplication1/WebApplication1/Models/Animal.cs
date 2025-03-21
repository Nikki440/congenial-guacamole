namespace WebApplication1.Models
{
    public class Animal
    {
        public int AnimalId { get; set; } // Auto-generated Id
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;

        // Foreign Key for Category
        public int? CategoryId { get; set; }  // Nullable foreign key
        public Category? Category { get; set; }

        // Foreign Key for Enclosure
        public int? EnclosureId { get; set; }  // Nullable foreign key
        public Enclosure? Enclosure { get; set; }

        public SizeEnum Size { get; set; }
        public DietaryEnum DietaryClass { get; set; }
        public ActivityPatternEnum ActivityPattern { get; set; }
        public bool Prey { get; set; }
        public int SpaceRequirement { get; set; }
        public SecurityLevelEnum SecurityRequirement { get; set; }
    }

    public enum DietaryEnum
    {
        Carnivore,
        Herbivore,
        Omnivore
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
        Microscopic, VerySmall, Small, Medium, Large, VeryLarge
    }
    public enum ActivityPatternEnum
    {
        Diurnal, Nocturnal, Cathemeral
    }
    public enum SecurityLevelEnum
    {
        Low, Medium, High,
    }
}