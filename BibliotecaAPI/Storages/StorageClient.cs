using BibliotecaAPI.Data;
using BibliotecaAPI.Models;
using Microsoft.Data.Sqlite;

namespace BibliotecaAPI.Storages
{
    public class StorageClient
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public StorageClient()
        {
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async void AddClient(Client client)
        {
            await using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    INSERT INTO Transacoes (updated_at, name, email, phone) 
                    VALUES (@UpdatedAt, @Name, @Email, @Phone);
                ";

                command.Parameters.AddWithValue("@UpdatedAt", client.UpdatedAt);
                command.Parameters.AddWithValue("@Name", client.Name);
                command.Parameters.AddWithValue("@Email", client.Email);
                command.Parameters.AddWithValue("@Phone", client.Phone);

                command.ExecuteNonQuery();
            }
        }

        // public Client GetClientById(string id)
        // {
        //     return Client
        // }

        // public List<Client> GetClients()
        // {
            
        // }

        // public void UpdateClient(Client client)
        // {
            
        // }

        // public void DeleteClient(Client client)
        // {
            
        // }

    }
}