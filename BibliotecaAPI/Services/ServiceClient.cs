using BibliotecaAPI.Interface;
using BibliotecaAPI.Models;
using BibliotecaAPI.Storages;
using Serilog;

namespace BibliotecaAPI.Services
{
    public class ServiceClient : IServiceGeneric<Client>
    {
        private readonly StorageClient _storage;

        public ServiceClient(StorageClient storage)
        {
            try
            {
                Log.Information("Creating a Storage instance.");
                _storage = storage;     // Cria uma instancia do Storage
                Log.Information("Storage instance created successfully.");
            }
            catch (Exception e)
            {
                Log.Error("Error creating Storage instance.");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para adicionar um novo Client
        public async Task AddAsync(Client client)
        {
            try
            {
                Log.Information("Method AddAsync of ServiceClient requested.");
                await _storage.AddAsync(client);
                Log.Information("Method AddAsync of ServiceClient completed sucessfully.");
            }
            catch (Exception e)
            {
                Log.Error($"Method AddAsync of ServiceClient request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para puxar do Db o Client especifico pelo Id
        public async Task<Client> GetByIdAsync(string id)
        {
            try
            {
                Log.Information("Method GetByIdAsync of ServiceClient requested.");
                var clientId = await _storage.GetByIdAsync(id);
                Log.Information("Method GetByIdAsync of ServiceClient completed sucessfully.");
                return clientId;
            }
            catch (Exception e)
            {
                Log.Error($"Method GetByIdAsync of ServiceClient request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para puxar do Db todos os Clients
        public async Task<List<Client>> GetsAsync()
        {
            try
            {
                Log.Information("Method GetsAsync of ServiceClient requested.");
                var clientsData = await _storage.GetsAsync();
                Log.Information("Method GetsAsync of ServiceClient completed sucessfully.");
                return clientsData;
            }
            catch (Exception e)
            {
                Log.Error($"Method GetsAsync of ServiceClient request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
            
        }

        // Metodo para atualizar uma Linha da tabela Client
        public async Task UpdateAsync(string id, Client client)
        {
            try
            {
                Log.Information("Method UpdateAsync of ServiceClient requested.");
                await _storage.UpdateAsync(id, client);
                Log.Information("Method UpdateAsync of ServiceClient completed sucessfully.");
            }
            catch (Exception e)
            {
                Log.Error($"Method UpdateAsync of ServiceClient request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para deletar uma Linha da tabela Client pelo id
        public async Task DeleteAsync(string id)
        {
            try
            {
                Log.Information("Method DeleteAsync of ServiceClient requested.");
                await _storage.DeleteAsync(id); 
                Log.Information("Method DeleteAsync of ServiceClient completed sucessfully.");
            }
            catch (Exception e)
            {
                Log.Error($"Method DeleteAsync of ServiceClient request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
        }
    }
}