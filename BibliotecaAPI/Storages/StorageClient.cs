using System.Globalization;
using BibliotecaAPI.Data;
using BibliotecaAPI.Interface.Storages;
using BibliotecaAPI.Models;
using Microsoft.Data.Sqlite;

namespace BibliotecaAPI.Storages
{
    public class StorageClient : IStorageClient
    {
        private readonly string _connectionString;

        public StorageClient(SQLiteDbConfig dbConfig) // Recebe o dbConfig
        {
            _connectionString = dbConfig.GetConnectionString();
        }

        public  async Task AddClientAsync(Client client)
        {
            await using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    INSERT INTO Client (id, created_at, updated_at, name, email, phone) 
                    VALUES (@Id, @CreatedAt, @UpdatedAt, @Name, @Email, @Phone);
                ";

                command.Parameters.AddWithValue("@Id", Ulid.NewUlid().ToString());
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.TimeOfDay);
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.TimeOfDay);
                command.Parameters.AddWithValue("@Name", client.Name);
                command.Parameters.AddWithValue("@Email", client.Email);
                command.Parameters.AddWithValue("@Phone", client.Phone);

                command.ExecuteNonQuery();
            }
        }

        public async Task<Client> GetClientByIdAsync(string id)
        {
            Client getClient = null;
            await using (var connection = new SqliteConnection(_connectionString))
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

        public async Task<List<Client>> GetClientsAsync()
        {
            List<Client> listClients = new List<Client>();
            Client getClient;
            await using (var connection = new SqliteConnection(_connectionString))
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

        public async Task UpdateClientAsync(string id, Client client)
        {
            await using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    UPDATE Client
                    SET updated_at = @UpdatedAt, name = @Name, email = @Email, phone = @Phone WHERE id = @Id;
                ";

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.TimeOfDay);
                command.Parameters.AddWithValue("@Name", client.Name);
                command.Parameters.AddWithValue("@Email", client.Email);
                command.Parameters.AddWithValue("@Phone", client.Phone);

                command.ExecuteNonQuery();
            }
        }

        public async Task DeleteClientAsync(string id)
        {
            await using (var connection = new SqliteConnection(_connectionString))
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