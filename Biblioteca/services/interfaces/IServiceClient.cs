public interface IServiceClient
{
    void RegisterClient(Client client);
    Client GetClient(string id);
    List<Client> GetClients();
    Client UpdateClient(Client client);
    Client DeleteClient(string id);
}