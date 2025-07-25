using BibliotecaAPI.Interface;
using BibliotecaAPI.Storages;

namespace BibliotecaAPI.Services
{
    public class ServiceInventory : IServiceGeneric<Inventory>
    {
        private readonly StorageInventory _storage;

        public ServiceInventory(StorageInventory storage)
        {
            _storage = storage;     // Cria uma instancia do Storage
        }

        // Metodo para adicionar um novo Inventory
        public async Task AddAsync(Inventory inventory)
        {
            await _storage.AddAsync(inventory);
        }

        // Metodo para puxar do Db o Inventory especifico pelo Id
        public async Task<Inventory> GetByIdAsync(string id)
        {
            return await _storage.GetByIdAsync(id);
        }

        // Metodo para puxar do Db todos os Inventory
        public async Task<List<Inventory>> GetsAsync()
        {
            return await _storage.GetsAsync();
        }

        // Metodo para atualizar uma Linha da tabela Inventory
        public async Task UpdateAsync(string id, Inventory inventory)
        {
            await _storage.UpdateAsync(id, inventory);
        }

        // Metodo para deletar uma Linha da tabela Inventory pelo id
        public async Task DeleteAsync(string id)
        {
            await _storage.DeleteAsync(id);
        }
    }
}