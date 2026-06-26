using DEADLOCK.APP.Models;

namespace DEADLOCK.APP.Repository
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order order);
        Task<Order?> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<bool> UpdateItemQuantityAsync(int orderId, int productId, int cantidad);
        Task<bool> RemoveItemAsync(int orderId, int productId);
    }
}
