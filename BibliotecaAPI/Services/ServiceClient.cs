using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaAPI.Interface;
using BibliotecaAPI.Models;
using BibliotecaAPI.Storages;

namespace BibliotecaAPI.Services
{
    public class ServiceClient : IServiceClient
    {
        private readonly StorageClient _storage;

        public ServiceClient(StorageClient storage)
        {
            _storage = storage;
        }

        public async Task AddClientAsync(Client client)
        {
           await _storage.AddClientAsync(client);
        }
        public async Task<Client> GetClientByIdAsync(string id)
        {
            return await _storage.GetClientByIdAsync(id);
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await _storage.GetClientsAsync();
        }

        public async Task UpdateClientAsync(string id, Client client)
        {
            await _storage.UpdateClientAsync(id, client);
        }

        public async Task DeleteClientAsync(string id)
        {
            await _storage.DeleteClientAsync(id);
        }
    }
}