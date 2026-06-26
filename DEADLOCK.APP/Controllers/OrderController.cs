using DEADLOCK.APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace DEADLOCK.APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            if (orderDto == null || orderDto.Items.Count < 2)
                return BadRequest("La orden debe tener al menos 2 productos.");

            var nuevaOrden = await _orderService.CreateAsync(orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = nuevaOrden.Id }, nuevaOrden);
        }


        [HttpPut("{orderId}/items/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateItemQuantity(int orderId, int productId, [FromBody] int cantidad)
        {
            var result = await _orderService.UpdateItemQuantityAsync(orderId, productId, cantidad);
            if (!result) return NotFound();
            return Ok("Cantidad actualizada correctamente.");
        }


        [HttpDelete("{orderId}/items/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveItem(int orderId, int productId)
        {
            var result = await _orderService.RemoveItemAsync(orderId, productId);
            if (!result) return NotFound();
            return Ok("Producto eliminado y stock actualizado.");
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var orden = await _orderService.GetByIdAsync(id);
            if (orden == null) return NotFound();
            return Ok(orden);
        }
    }
}
