using DEADLOCK.APP.Models;
using DEADLOCK.APP.Repository;

namespace DEADLOCK.APP.Services
{
    public class ClientesServices : IClientesServices
    {
        private readonly IClientesRepository _repository;

        public ClientesServices(IClientesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Clientes> CreateAsync(Clientes cliente)
        {
            return await _repository.CreateAsync(cliente);
        }

        public async Task<Clientes?> ClienteByName(string name)
        {
            var clientes = await _repository.GetAllAsync();
            return clientes.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var cliente = await _repository.GetByIdAsync(id);
            if (cliente == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<Clientes?> GetClienteByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Clientes>> GetAllClientesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateAsync(Clientes cliente)
        {
            await _repository.UpdateAsync(cliente);
        }
    }
}
