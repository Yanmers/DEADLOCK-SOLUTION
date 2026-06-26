using DEADLOCK.APP.Models;

namespace DEADLOCK.APP.Repository
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task UpdateStockAsync(int productId, int cantidad);
    }
}
