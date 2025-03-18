namespace WebApplication1.Models
{
    public class Enclosure
    {
        public int id { get; set; }
        public string name { get; set; }
        public string animal { get; set; }
        public habitatSizeEnum habitatSize { get; set; }
        public habitatWeatherEnum habitatweather { get; set; }
    }
    public enum habitatSizeEnum
    {
        Low,
        Medium,
        High
    }
    public enum habitatWeatherEnum
    {
        Sunny,
        Cloudy,
        Raining,
        Clear
    }
}
