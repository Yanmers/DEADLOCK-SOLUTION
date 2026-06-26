using DEADLOCK.APP.Data;
using DEADLOCK.APP.Models;
using Microsoft.EntityFrameworkCore;

namespace DEADLOCK.APP.Repository
{
    public class ClientesRepository : IClientesRepository
    {
        private readonly AppDbContext _context;

        public ClientesRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Clientes> CreateAsync(Clientes cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Clientes?> GetByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }


        public async Task<IEnumerable<Clientes>> GetAllAsync()
        {
            return await Task.FromResult(_context.Clientes.ToList());
        }


        public async Task UpdateAsync(Clientes cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }

    }
}
