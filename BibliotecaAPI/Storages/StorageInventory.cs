using BibliotecaAPI.Data;
using BibliotecaAPI.Interface.Storages;
using Microsoft.Data.Sqlite;

namespace BibliotecaAPI.Storages
{
    public class StorageInventory : IStorageGeneric<Inventory>
    {
        private readonly string _connectionString;

        public StorageInventory(SQLiteDbConfig dbConfig)
        {
            _connectionString = dbConfig.GetConnectionString();
        }

        public async Task AddAsync(Inventory inventory)
        {
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
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
                
                command.ExecuteNonQuery();
            }
        }

        public async Task<Inventory> GetByIdAsync(string id)
        {
            Inventory getInventory = null;
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    SELECT * FROM Inventory WHERE id = @Id;
                ";

                command.Parameters.AddWithValue("@Id", id);

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
            return getInventory;
        }

        public async Task<List<Inventory>> GetsAsync()
        {
            List<Inventory> listInventory = new List<Inventory>();
            Inventory getInventory;
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    SELECT * FROM Inventory;
                ";

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
            return listInventory;
        }

        public async Task UpdateAsync(string id, Inventory inventory)
        {
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
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
                    DELETE FROM Inventory
                    WHERE id = @Id;
                ";

                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}