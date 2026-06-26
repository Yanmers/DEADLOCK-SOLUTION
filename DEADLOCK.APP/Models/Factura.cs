namespace DEADLOCK.APP.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Total { get; set; }


        public Clientes Cliente { get; set; }
        public ICollection<FacturaItem> Items { get; set; } = new List<FacturaItem>();


    }
}
