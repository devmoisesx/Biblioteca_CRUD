using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Interface.Storages
{
    public interface IStorageClient
    {
        Task AddClientAsync(Client client);
        Task<Client> GetClientByIdAsync(string id);
        Task<List<Client>> GetClientsAsync();
        Task UpdateClientAsync(string id, Client client);
        Task DeleteClientAsync(string id);
    }
}