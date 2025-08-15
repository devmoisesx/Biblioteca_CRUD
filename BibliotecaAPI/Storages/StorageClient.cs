using BibliotecaAPI.Data;
using BibliotecaAPI.Interface.Storages;
using BibliotecaAPI.Models;
using Microsoft.Data.Sqlite;
using Serilog;

namespace BibliotecaAPI.Storages
{
    public class StorageClient : IStorageGeneric<Client>
    {
        private readonly string _connectionString;

        public StorageClient(SQLiteDbConfig dbConfig)
        {
try
            {
                Log.Information("Pulling the Database Connection String.");
                _connectionString = dbConfig.GetConnectionString();     // Puxa a string de conexao do Db
                Log.Information("Database connection string pulled successfully.");
            }
            catch (Exception e)
            {
                Log.Error("Error pulling the Database Connection String.");
                throw new Exception($"Erro: {e.Message}");
            }        }

        // Metodo para Adicionar uma nova linha na Tabela
        public async Task AddAsync(Client client)
        {
            try
            {
                Log.Information("Adding a new row to the table.");
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

                    Log.Information("Executing SQL command in the Database.");
                    command.ExecuteNonQuery();
                }
                Log.Information("SQL command executed successfully.");
            }
            catch (Exception e)
            {
                Log.Error("Error adding a new row to the table.");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para puxar uma linha da Tabela pelo ID
        public async Task<Client> GetByIdAsync(string id)
        {
            try
            {
                Log.Information("Getting a row from the table by id.");
                Client getClient = null;
                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        SELECT * FROM Client WHERE id = @Id;
                    ";

                    command.Parameters.AddWithValue("@Id", id);

                    Log.Information("Executing SQL command.");
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
                Log.Information("SQL command executed successfully.");
                return getClient;
            }
            catch (Exception e)
            {
                Log.Error("Error adding a new row to the table.");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para puxar todos os dados da Tabela
        public async Task<List<Client>> GetsAsync()
        {
            try
            {
                Log.Information("Getting all Data from the table.");
                List<Client> listClients = new List<Client>();
                Client getClient;
                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        SELECT * FROM Client;
                    ";

                    Log.Information("Executing SQL command.");
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
                Log.Information("SQL command executed successfully.");
                return listClients;
            }
            catch (Exception e)
            {
                Log.Error("Error adding a new row to the table.");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para atualizar uma linha da Tabela pelo ID
        public async Task UpdateAsync(string id, Client client)
        {
            try
            {
                Log.Information("Updating a table row by Id.");
                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
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

                    Log.Information("Executing SQL command.");
                    await command.ExecuteNonQueryAsync();
                    Log.Information("SQL command executed successfully.");
                }
            }
            catch (Exception e)
            {
                Log.Error("Error updating a table row by Id.");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para Deletar uma linha da Tabela pelo ID
        public async Task DeleteAsync(string id)
        {
            try
            {
                Log.Information("Deleting a row from the table by Id.");
                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        DELETE FROM Client
                        WHERE id = @Id;
                    ";

                    command.Parameters.AddWithValue("@Id", id);

                    Log.Information("Executing SQL command.");
                    await command.ExecuteNonQueryAsync();
                    Log.Information("SQL command executed successfully.");
                }
            }
            catch (Exception e)
            {
                Log.Error("Error deleting a row from the table by Id.");
                throw new Exception($"Erro: {e.Message}");
            }
        }
    }
}