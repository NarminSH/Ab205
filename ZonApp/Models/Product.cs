namespace ZonApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainImageUrl { get; set; }
        public decimal Price { get; set; } 
        public bool InStock { get; set; }
        public int Count { get; set; }
        

    }
}
