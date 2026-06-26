using DEADLOCK.APP.Models;
using DEADLOCK.APP.Repository;
using Shared.Dtos;

namespace DEADLOCK.APP.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<Order> CreateAsync(OrderDto dto)
        {
            var order = new Order
            {
                ClienteId = dto.ClienteId,
                Fecha = DateTime.Now
            };

            foreach (var item in dto.Items)
            {
                var producto = await _productRepository.GetByIdAsync(item.ProductoId);
                if (producto == null || producto.Stock < item.Cantidad)
                    throw new Exception("Stock insuficiente.");

                producto.Stock -= item.Cantidad;

                order.Items.Add(new OrderItem
                {
                    ProductoId = item.ProductoId,
                    Cantidad = item.Cantidad,
                    Precio = producto.Precio
                });
            }

            return await _orderRepository.CreateAsync(order);
        }

        public Task<bool> UpdateItemQuantityAsync(int orderId, int productId, int cantidad)
        {
            return _orderRepository.UpdateItemQuantityAsync(orderId, productId, cantidad);
        }


        public Task<bool> RemoveItemAsync(int orderId, int productId)
        {
            return _orderRepository.RemoveItemAsync(orderId, productId);
        }


        public Task<Order?> GetByIdAsync(int id)
        {
            return _orderRepository.GetByIdAsync(id);
        }


        public Task<IEnumerable<Order>> GetAllAsync()
        {
            return _orderRepository.GetAllAsync();
        }

    }
}
