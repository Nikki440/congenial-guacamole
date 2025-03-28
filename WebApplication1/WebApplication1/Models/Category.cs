﻿
namespace WebApplication1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Animal> Animals { get; set; } = new(); // Relatie naar dieren
    }
}
