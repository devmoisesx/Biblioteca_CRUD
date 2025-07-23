using BibliotecaAPI.Models;

namespace BibliotecaAPI.Interface
{
    public interface IServiceClient
    {
        Task AddClientAsync(Client client);
        Task<Client> GetClientByIdAsync(string id);
        Task<List<Client>> GetClientsAsync();
        Task UpdateClientAsync(string id, Client client);
        TaskScheduler DeleteClientAsync(string id);
    }
}