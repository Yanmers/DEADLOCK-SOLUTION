using DEADLOCK.APP.Data;
using DEADLOCK.APP.Models;
using Microsoft.EntityFrameworkCore;

namespace DEADLOCK.APP.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }


        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.Include(o => o.Items).ToListAsync();
        }


        public async Task<bool> UpdateItemQuantityAsync(int orderId, int productId, int cantidad)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            var item = order?.Items.FirstOrDefault(i => i.ProductoId == productId);
            if (item == null) return false;

            item.Cantidad = cantidad;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveItemAsync(int orderId, int productId)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            var item = order?.Items.FirstOrDefault(i => i.ProductoId == productId);
            if (item == null) return false;

            order.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
