using BibliotecaAPI.Interface;
using BibliotecaAPI.Models;
using BibliotecaAPI.Storages;

namespace BibliotecaAPI.Services
{
    public class ServiceClient : IServiceGeneric<Client>
    {
        private readonly StorageClient _storage;

        public ServiceClient(StorageClient storage)
        {
            _storage = storage;
        }

        public async Task AddAsync(Client client)
        {
           await _storage.AddAsync(client);
        }
        public async Task<Client> GetByIdAsync(string id)
        {
            return await _storage.GetByIdAsync(id);
        }

        public async Task<List<Client>> GetsAsync()
        {
            return await _storage.GetsAsync();
        }

        public async Task UpdateAsync(string id, Client client)
        {
            await _storage.UpdateAsync(id, client);
        }

        public async Task DeleteAsync(string id)
        {
            await _storage.DeleteAsync(id);
        }
    }
}