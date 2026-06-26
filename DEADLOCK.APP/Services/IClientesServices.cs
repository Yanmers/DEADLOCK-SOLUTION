using DEADLOCK.APP.Models;

namespace DEADLOCK.APP.Services
{
    public interface IClientesServices
    {
        Task<Clientes> CreateAsync(Clientes cliente);
        Task<Clientes?> GetClienteByIdAsync(int id);
        Task<IEnumerable<Clientes>> GetAllClientesAsync();
        Task<Clientes?> ClienteByName(string name);
        Task UpdateAsync(Clientes cliente);
        Task<bool> DeleteClienteAsync(int id);
    }
}
