using BibliotecaAPI.Data;
using BibliotecaAPI.Interface.Storages;
using Microsoft.Data.Sqlite;
using Serilog;

namespace BibliotecaAPI.Storages
{
    public class StorageInventory : IStorageGeneric<Inventory>
    {
        private readonly string _connectionString;

        public StorageInventory(SQLiteDbConfig dbConfig)
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
            }
        }

        // Metodo para Adicionar uma nova linha na Tabela
        public async Task AddAsync(Inventory inventory)
        {
            try
            {
                Log.Information("Adding a new row to the table.");

                if (!int.IsEvenInteger(inventory.Condition))
                {
                    Log.Error("Invalid Condition.");
                    throw new ArgumentException("Invalid inventory Condition.");
                }

                if (!int.IsEvenInteger(inventory.Is_Avaible))
                {
                    Log.Error("Invalid Is Avaible.");
                    throw new ArgumentException("Invalid inventory Is Avaible.");
                }

                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        INSERT INTO Inventory (id, created_at, updated_at, condition, is_avaible, catalog_id) 
                        VALUES (@Id, @CreatedAt, @UpdatedAt, @Condition, @Is_Avaible, @Catalog_Id);
                    ";

                    command.Parameters.AddWithValue("@Id", Ulid.NewUlid().ToString());
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.TimeOfDay);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.TimeOfDay);
                    command.Parameters.AddWithValue("@Condition", inventory.Condition);
                    command.Parameters.AddWithValue("@Is_Avaible", inventory.Is_Avaible);
                    command.Parameters.AddWithValue("@Catalog_Id", inventory.Catalog_Id);

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
        public async Task<Inventory> GetByIdAsync(string id)
        {
            try
            {
                Log.Information("Getting a row from the table by id.");
                Inventory getInventory = null;
                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        SELECT * FROM Inventory WHERE id = @Id;
                    ";

                    command.Parameters.AddWithValue("@Id", id);

                    Log.Information("Executing SQL command.");
                    await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            getInventory = new Inventory(
                                reader.GetString(0),
                                reader.GetTimeSpan(1),
                                reader.GetTimeSpan(2),
                                reader.GetString(3),
                                reader.GetInt32(4),
                                reader.GetInt16(5)
                            );
                        }
                    }
                }
                if (getInventory == null)
                {
                    Log.Error("Invalid Inventory Id.");
                    throw new ArgumentException("Invalid Inventory Id.");
                }
                Log.Information("SQL command executed successfully.");
                return getInventory;
            }
            catch (Exception e)
            {
                Log.Error("Error getting a row from the table by id.");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para puxar todos os dados da Tabela
        public async Task<List<Inventory>> GetsAsync()
        {
            try
            {
                Log.Information("Getting all Data from the table.");
                List<Inventory> listInventory = new List<Inventory>();
                Inventory getInventory;
                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        SELECT * FROM Inventory;
                    ";

                    Log.Information("Executing SQL command.");
                    await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            getInventory = new Inventory(
                                reader.GetString(0),
                                reader.GetTimeSpan(1),
                                reader.GetTimeSpan(2),
                                reader.GetString(3),
                                reader.GetInt32(4),
                                reader.GetInt16(5)
                            );
                            listInventory.Add(getInventory);
                        }
                    }
                }
                Log.Information("SQL command executed successfully.");
                return listInventory;
            }
            catch (Exception e)
            {
                Log.Error("Error getting all Data from the table.");
                throw new Exception($"Erro: {e.Message}");
            }
        }

       // Metodo para atualizar uma linha da Tabela pelo ID
        public async Task UpdateAsync(string id, Inventory inventory)
        {
            try
            {
                Log.Information("Updating a table row by Id.");

                if (!int.IsEvenInteger(inventory.Condition))
                {
                    Log.Error("Invalid Condition.");
                    throw new ArgumentException("Invalid inventory Condition.");
                }

                if (!int.IsEvenInteger(inventory.Is_Avaible))
                {
                    Log.Error("Invalid Is Avaible.");
                    throw new ArgumentException("Invalid inventory Is Avaible.");
                }

                if (!string.IsNullOrEmpty(id))
                {
                    Log.Error("Invalid Inventory ID.");
                    throw new ArgumentException("Invalid Inventory ID.");
                }

                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        UPDATE Inventory
                        SET updated_at = @UpdatedAt, condition = @Condition, is_avaible = @Is_Avaible, catalog_id = @Catalog_Id WHERE id = @Id;;
                    ";

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.TimeOfDay);
                    command.Parameters.AddWithValue("@Condition", inventory.Condition);
                    command.Parameters.AddWithValue("@Is_Avaible", inventory.Is_Avaible);
                    command.Parameters.AddWithValue("@Catalog_Id", inventory.Catalog_Id);

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

                if (!string.IsNullOrEmpty(id))
                {
                    Log.Error("Invalid Inventory ID.");
                    throw new ArgumentException("Invalid Inventory ID.");
                }

                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        DELETE FROM Inventory
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