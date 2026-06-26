using DEADLOCK.APP.Models;
using Shared.Dtos;

namespace DEADLOCK.APP.Services
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(OrderDto dto);
        Task<bool> UpdateItemQuantityAsync(int orderId, int productId, int cantidad);
        Task<bool> RemoveItemAsync(int orderId, int productId);
        Task<Order?> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
    }
}
