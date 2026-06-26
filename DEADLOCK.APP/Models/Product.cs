namespace DEADLOCK.APP.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        // un producto puede estar en muchos ítems de orden
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
