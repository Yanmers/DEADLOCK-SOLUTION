using DEADLOCK.APP.Models;

namespace DEADLOCK.APP.Repository
{
    public interface IFacturaRepository
    {
        Task<Factura> CreateAsync(Factura factura);
        Task<Factura?> GetByIdAsync(int id);
        Task<IEnumerable<Factura>> GetAllAsync();
    }
}
