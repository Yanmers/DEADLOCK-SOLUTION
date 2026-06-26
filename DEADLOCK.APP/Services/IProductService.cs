using DEADLOCK.APP.Models;

namespace DEADLOCK.APP.Services
{
    public interface IProductService
    {
        Task<Product> CreateAsync(Product producto);
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task UpdateAsync(Product producto);
        Task<bool> DeleteAsync(int id);
        Task UpdateStockAsync(int productId, int cantidad);
    }
}
