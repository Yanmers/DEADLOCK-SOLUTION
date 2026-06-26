using DEADLOCK.APP.Models;
using DEADLOCK.APP.Repository;
using Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DEADLOCK.APP.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IClientesRepository _clientesRepository;

        public FacturaService(IFacturaRepository facturaRepository, IOrderRepository orderRepository, IClientesRepository clientesRepository)
        {
            _facturaRepository = facturaRepository;
            _orderRepository = orderRepository;
            _clientesRepository = clientesRepository;
        }

        public async Task<FacturaDto> GenerarFacturaAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null) throw new Exception("Orden no encontrada.");

            var cliente = await _clientesRepository.GetByIdAsync(order.ClienteId);
            if (cliente == null) throw new Exception("Cliente no encontrado.");

            var factura = new Factura
            {
                ClienteId = order.ClienteId,
                Fecha = DateTime.Now,
                Items = order.Items.Select(i => new FacturaItem
                {
                    ProductoId = i.ProductoId,
                    Cantidad = i.Cantidad,
                    PrecioUnitario = i.Precio,
                    Subtotal = i.Cantidad * i.Precio
                }).ToList(),
                Total = order.Items.Sum(i => i.Cantidad * i.Precio)
            };

            await _facturaRepository.CreateAsync(factura);

            return new FacturaDto
            {
                Id = factura.Id,
                ClienteId = factura.ClienteId,
                Fecha = factura.Fecha,
                Total = factura.Total,
                Items = factura.Items.Select(i => new FacturaItemDto
                {
                    ProductoId = i.ProductoId,
                    Cantidad = i.Cantidad,
                    PrecioUnitario = i.PrecioUnitario,
                    Subtotal = i.Subtotal
                }).ToList()
            };
        }

        public async Task<FacturaDto?> GetByIdAsync(int id)
        {
            var factura = await _facturaRepository.GetByIdAsync(id);
            if (factura == null) return null;

            return new FacturaDto
            {
                Id = factura.Id,
                ClienteId = factura.ClienteId,
                Fecha = factura.Fecha,
                Total = factura.Total,
                Items = factura.Items.Select(i => new FacturaItemDto
                {
                    ProductoId = i.ProductoId,
                    Cantidad = i.Cantidad,
                    PrecioUnitario = i.PrecioUnitario,
                    Subtotal = i.Subtotal
                }).ToList()
            };
        }

        public async Task<IEnumerable<FacturaDto>> GetAllAsync()
        {
            var facturas = await _facturaRepository.GetAllAsync();
            return facturas.Select(f => new FacturaDto
            {
                Id = f.Id,
                ClienteId = f.ClienteId,
                Fecha = f.Fecha,
                Total = f.Total,
                Items = f.Items.Select(i => new FacturaItemDto
                {
                    ProductoId = i.ProductoId,
                    Cantidad = i.Cantidad,
                    PrecioUnitario = i.PrecioUnitario,
                    Subtotal = i.Subtotal
                }).ToList()
            });
        }
    }
}
