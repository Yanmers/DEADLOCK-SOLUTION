using DEADLOCK.APP.Models;
using DEADLOCK.APP.Repository;

namespace DEADLOCK.APP.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> CreateAsync(Product producto)
        {
            var entity = await _repository.GetByIdAsync(producto.Id);
            if (entity != null)
                throw new Exception("El producto ya existe.");

            return producto;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateAsync(Product producto)
        {
            await _repository.UpdateStockAsync(producto.Id, producto.Stock);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var producto = await _repository.GetByIdAsync(id);
            if (producto == null) return false;

            //implementar un metodo si es necesario para eliminar
            return true;
        }

        public async Task UpdateStockAsync(int productId, int cantidad)
        {
            await _repository.UpdateStockAsync(productId, cantidad);
        }
    }
}
