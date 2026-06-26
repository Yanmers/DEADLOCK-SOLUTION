namespace DEADLOCK.APP.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }


        public Order Order { get; set; }
        public Product Producto { get; set; }
    }
}
