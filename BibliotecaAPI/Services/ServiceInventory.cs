using BibliotecaAPI.Interface;
using BibliotecaAPI.Storages;

namespace BibliotecaAPI.Services
{
    public class ServiceInventory : IServiceGeneric<Inventory>
    {
        private readonly StorageInventory _storage;

        public ServiceInventory(StorageInventory storage)
        {
            _storage = storage;
        }

        public async Task AddAsync(Inventory inventory)
        {
           await _storage.AddAsync(inventory);
        }
        public async Task<Inventory> GetByIdAsync(string id)
        {
            return await _storage.GetByIdAsync(id);
        }

        public async Task<List<Inventory>> GetsAsync()
        {
            return await _storage.GetsAsync();
        }

        public async Task UpdateAsync(string id, Inventory inventory)
        {
            await _storage.UpdateAsync(id, inventory);
        }

        public async Task DeleteAsync(string id)
        {
            await _storage.DeleteAsync(id);
        }
    }
}