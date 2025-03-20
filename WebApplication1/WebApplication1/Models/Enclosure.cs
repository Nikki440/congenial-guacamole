namespace WebApplication1.Models
{
    public class Enclosure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Animal> Animals { get; set; } = new();
        public ClimateEnum Climate { get; set; }
        public flagsEnum HabitatType { get; set; }
        public SecurityLevelEnum SecurityLevel { get; set; }
        public double Size { get; set; }
 
    }

    public enum ClimateEnum
    {
        Tropical, Temperate, Arctic
    }
    public enum flagsEnum
    {
        Forest = 1, Aquatic = 2, Desert = 4, Grassland = 8
    }
    public enum SecurityLevel
    {
        Low, Medium, High
    }



}
