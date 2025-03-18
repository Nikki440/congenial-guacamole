namespace WebApplication1.Models
{
    public class Enclosure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Animal { get; set; }
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
        Forest, Aquatic, Desert, Grassland
    }
    public enum SecurityLevel
    {
        Low, Medium, High
    }

}
