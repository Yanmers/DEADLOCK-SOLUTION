using DEADLOCK.APP.Models;
using DEADLOCK.APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace DEADLOCK.APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionClientesController : ControllerBase
    {
        private readonly IClientesServices _clientesServices;

        public GestionClientesController(IClientesServices clientesServices)
        {
            _clientesServices = clientesServices;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCliente([FromBody] ClienteDto cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Cliente no válido");
            }

            var nuevoCliente = new Clientes
            {
                Name = cliente.Name,
                Email = cliente.Email
            };

            await _clientesServices.CreateAsync(nuevoCliente);


            return CreatedAtAction(nameof(GetClienteById), new { id = nuevoCliente.Id }, nuevoCliente);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteById(int id)
        {
            var cliente = await _clientesServices.GetClienteByIdAsync(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }
    }
}
