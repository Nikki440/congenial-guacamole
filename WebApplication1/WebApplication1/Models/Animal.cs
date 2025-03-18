namespace WebApplication1.Models
{
    public class Animal
    {

        public int id { get; set; }
        public string name { get; set; }
        public DateTime Birthday { get; set; }
        public speciesEnum species { get; set; }
        public dietEnum diet { get; set; }
    }

    public enum dietEnum
    {
        Carnovore,
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
}
