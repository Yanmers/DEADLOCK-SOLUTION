using System.Data;

namespace DEADLOCK.APP.Models
{
    public class Clientes
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Relación: un cliente puede tener muchas órdenes
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public DateTime RegistroUpDate { get; set; }
    }
}
