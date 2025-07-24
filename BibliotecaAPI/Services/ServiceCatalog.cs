using BibliotecaAPI.Interface;
using BibliotecaAPI.Storages;

namespace BibliotecaAPI.Services
{
    public class ServiceCatalog : IServiceGeneric<Catalog>
    {
        private readonly StorageCatalog _storage;

        public ServiceCatalog(StorageCatalog storage)
        {
            _storage = storage;
        }

        public async Task AddAsync(Catalog catalog)
        {
           await _storage.AddAsync(catalog);
        }
        public async Task<Catalog> GetByIdAsync(string id)
        {
            return await _storage.GetByIdAsync(id);
        }

        public async Task<List<Catalog>> GetsAsync()
        {
            return await _storage.GetsAsync();
        }

        public async Task UpdateAsync(string id, Catalog catalog)
        {
            await _storage.UpdateAsync(id, catalog);
        }

        public async Task DeleteAsync(string id)
        {
            await _storage.DeleteAsync(id);
        }
    }
}