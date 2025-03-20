namespace WebApplication1.Models
{
    public class Zoo
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Congenial-guacamole dierentuin";
        public List<Animal> Animals { get; set; } = new();
        public List<Enclosure> Enclosures { get; set; } = new();
    }
}
