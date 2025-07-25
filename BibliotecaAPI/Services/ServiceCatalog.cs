using BibliotecaAPI.Interface;
using BibliotecaAPI.Storages;

namespace BibliotecaAPI.Services
{
    public class ServiceCatalog : IServiceGeneric<Catalog>
    {
        private readonly StorageCatalog _storage;

        public ServiceCatalog(StorageCatalog storage)
        {
            _storage = storage;     // Cria uma instancia do Storage
        }

        // Metodo para adicionar um novo Catalog
        public async Task AddAsync(Catalog catalog)
        {
            await _storage.AddAsync(catalog);
        }

        // Metodo para puxar do Db o Catalog especifico pelo Id
        public async Task<Catalog> GetByIdAsync(string id)
        {
            return await _storage.GetByIdAsync(id);
        }

        // Metodo para puxar do Db todos os Catalogs
        public async Task<List<Catalog>> GetsAsync()
        {
            return await _storage.GetsAsync();
        }

        // Metodo para atualizar uma Linha da tabela Catalog
        public async Task UpdateAsync(string id, Catalog catalog)
        {
            await _storage.UpdateAsync(id, catalog);
        }

        // Metodo para deletar uma Linha da tabela Catalog pelo id
        public async Task DeleteAsync(string id)
        {
            await _storage.DeleteAsync(id);
        }
    }
}