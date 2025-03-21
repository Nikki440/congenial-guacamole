
namespace WebApplication1.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Animal> Animals { get; set; } = new(); // Relatie naar dieren

        public static implicit operator string(Category v)
        {
            throw new NotImplementedException();
        }
    }
}
