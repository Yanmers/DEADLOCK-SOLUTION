using Shared.Dtos;

namespace DEADLOCK.APP.Services
{
    public interface IFacturaService
    {
        Task<FacturaDto> GenerarFacturaAsync(int orderId);
        Task<FacturaDto?> GetByIdAsync(int id);
        Task<IEnumerable<FacturaDto>> GetAllAsync();
    }
}
