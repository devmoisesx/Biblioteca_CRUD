using BibliotecaAPI.Data;
using BibliotecaAPI.Models;
using Microsoft.AspNetCore.Mvc;
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

        public Client GetClientById(string id)
        {
            Client getClient = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    SELECT * FROM Client WHERE id = @Id;
                ";

                command.Parameters.AddWithValue("@Id", id);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        getClient = new Client(
                            reader.GetString(0),
                            reader.GetTimeSpan(1),
                            reader.GetTimeSpan(2),
                            reader.GetString(3),
                            reader.GetString(4),
                            reader.GetString(5)
                        );
                    }
                }
            }
            return getClient;
        }

        public List<Client> GetClients(string id)
        {
            List<Client> listClients = new List<Client>();
            Client getClient;
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    SELECT * FROM Client;
                ";

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        getClient = new Client(
                            reader.GetString(0),
                            reader.GetTimeSpan(1),
                            reader.GetTimeSpan(2),
                            reader.GetString(3),
                            reader.GetString(4),
                            reader.GetString(5)
                        );
                        listClients.Add(getClient);
                    }
                }
            }
            return listClients;
        }

        public void UpdateClient(string id, Client client)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    UPDATE Client
                    SET updated_at = @UpdatedAt,name = @Name, email = @Email, phone = @Phone,
                    WHERE id = @Id;
                ";

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@UpdatedAt", client.UpdatedAt);
                command.Parameters.AddWithValue("@Name", client.Name);
                command.Parameters.AddWithValue("@Email", client.Email);
                command.Parameters.AddWithValue("@Phone", client.Phone);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteClient(string id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    DELETE FROM Client
                    WHERE id = @Id;
                ";

                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }

    }
}