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
    }
}