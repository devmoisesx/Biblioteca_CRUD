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
            _storage = storage;     // Cria uma instancia do Storage
        }

        // Metodo para adicionar um novo Catalog
        public async Task AddAsync(Client client)
        {
            await _storage.AddAsync(client);
        }

        // Metodo para puxar do Db o Catalog especifico pelo Id
        public async Task<Client> GetByIdAsync(string id)
        {
            return await _storage.GetByIdAsync(id);
        }

        // Metodo para puxar do Db todos os Catalogs
        public async Task<List<Client>> GetsAsync()
        {
            return await _storage.GetsAsync();
        }

        // Metodo para atualizar uma Linha da tabela Catalog
        public async Task UpdateAsync(string id, Client client)
        {
            await _storage.UpdateAsync(id, client);
        }

        // Metodo para deletar uma Linha da tabela Catalog pelo id
        public async Task DeleteAsync(string id)
        {
            await _storage.DeleteAsync(id);
        }
    }
}