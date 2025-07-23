using BibliotecaAPI.Models;

namespace BibliotecaAPI.Interface
{
    public interface IServiceClient
    {
        void AddClient(Client client);
        Client GetClientById(string id);
        List<Client> GetClients();
        void UpdateClient(string id, Client client);
        void DeleteClient(string id);
    }
}