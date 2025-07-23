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

        public void AddClient(Client client)
        {
            _storage.AddClient(client);
        }
        public Client GetClientById(string id)
        {
            return _storage.GetClientById(id);
        }

        public List<Client> GetClients()
        {
            return _storage.GetClients();
        }

        public void UpdateClient(string id, Client client)
        {
            _storage.UpdateClient(id, client);
        }

        public void DeleteClient(string id)
        {
            _storage.DeleteClient(id);
        }
    }
}