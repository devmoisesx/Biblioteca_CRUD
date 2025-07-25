using BibliotecaAPI.Data;
using BibliotecaAPI.Interface.Storages;
using BibliotecaAPI.Models;
using Microsoft.Data.Sqlite;

namespace BibliotecaAPI.Storages
{
    public class StorageClient : IStorageGeneric<Client>
    {
        private readonly string _connectionString;

        public StorageClient(SQLiteDbConfig dbConfig) // Recebe o dbConfig
        {
            _connectionString = dbConfig.GetConnectionString();
        }

        public  async Task AddAsync(Client client)
        {
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
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

        public async Task<Client> GetByIdAsync(string id)
        {
            Client getClient = null;
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    SELECT * FROM Client WHERE id = @Id;
                ";

                command.Parameters.AddWithValue("@Id", id);

                await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
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

        public async Task<List<Client>> GetsAsync()
        {
            List<Client> listClients = new List<Client>();
            Client getClient;
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    SELECT * FROM Client;
                ";

                await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
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

        public async Task UpdateAsync(string id, Client client)
        {
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
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

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(string id)
        {
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    DELETE FROM Client
                    WHERE id = @Id;
                ";

                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}