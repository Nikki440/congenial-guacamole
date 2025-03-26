using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class AnimalsInEnclosureViewModel
    {
        public Enclosure Enclosure { get; set; }
        public List<Animal> Animals { get; set; }
    }
}
