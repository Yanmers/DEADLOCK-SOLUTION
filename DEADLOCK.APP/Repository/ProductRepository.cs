using DEADLOCK.APP.Data;
using DEADLOCK.APP.Models;
using Microsoft.EntityFrameworkCore;

namespace DEADLOCK.APP.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }


        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }


        public async Task UpdateStockAsync(int productId, int cantidad)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.Stock = cantidad;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
