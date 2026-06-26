using DEADLOCK.APP.Data;
using DEADLOCK.APP.Models;
using Microsoft.EntityFrameworkCore;

namespace DEADLOCK.APP.Repository
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly AppDbContext _context;

        public FacturaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Factura> CreateAsync(Factura factura)
        {
            await _context.Facturas.AddAsync(factura);
            await _context.SaveChangesAsync();
            return factura;
        }

        public async Task<Factura?> GetByIdAsync(int id)
        {
            return await _context.Facturas
                .Include(f => f.Items)
                .FirstOrDefaultAsync(f => f.Id == id);
        }


        public async Task<IEnumerable<Factura>> GetAllAsync()
        {
            return await _context.Facturas.Include(f => f.Items).ToListAsync();
        }


        public async Task UpdateAsync(Factura factura)
        {
            _context.Facturas.Update(factura);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
                await _context.SaveChangesAsync();
            }
        }
    }
}
