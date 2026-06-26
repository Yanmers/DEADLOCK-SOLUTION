using DEADLOCK.APP.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Collections;

namespace DEADLOCK.APP.Repository
{
    public interface IClientesRepository
    {
        Task<Clientes> CreateAsync(Clientes clientes);

        Task UpdateAsync(Clientes clientes);

        Task<Clientes?> GetByIdAsync(int id);
        Task<IEnumerable<Clientes>> GetAllAsync();
        Task DeleteAsync(int id);


    }
}
