using BibliotecaAPI.Data;
using BibliotecaAPI.Interface.Storages;
using Microsoft.Data.Sqlite;
using Serilog;

namespace BibliotecaAPI.Storages
{
    public class StorageCatalog : IStorageGeneric<Catalog>
    {
        private readonly string _connectionString;

        public StorageCatalog(SQLiteDbConfig dbConfig)
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
        public async Task AddAsync(Catalog catalog)
        {
            try
            {
                Log.Information("Adding a new row to the table.");
                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        INSERT INTO Catalog (id, created_at, updated_at, title, author, year, rev, publisher_id, pages, synopsis, language_id, is_foreign) 
                        VALUES (@Id, @CreatedAt, @UpdatedAt, @Title, @Author, @Year, @Rev, @Publisher_Id, @Pages, @Synopsis, @Language_Id, @Is_Foreign);
                    ";

                    command.Parameters.AddWithValue("@Id", Ulid.NewUlid().ToString());
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.TimeOfDay);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.TimeOfDay);
                    command.Parameters.AddWithValue("@Title", catalog.Title);
                    command.Parameters.AddWithValue("@Author", catalog.Author);
                    command.Parameters.AddWithValue("@Year", catalog.Year);
                    command.Parameters.AddWithValue("@Rev", catalog.Rev);
                    command.Parameters.AddWithValue("@Publisher_Id", catalog.Publisher_Id);
                    command.Parameters.AddWithValue("@Pages", catalog.Pages);
                    command.Parameters.AddWithValue("@Synopsis", catalog.Synopsis);
                    command.Parameters.AddWithValue("@Language_Id", catalog.Language_Id);
                    command.Parameters.AddWithValue("@Is_Foreign", catalog.Is_Foreign);

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
        public async Task<Catalog> GetByIdAsync(string id)
        {
            try
            {
                Log.Information("Getting a row from the table by id.");
                Catalog getCatalog = null;
                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        SELECT * FROM Catalog WHERE id = @Id;
                    ";

                    command.Parameters.AddWithValue("@Id", id);

                    Log.Information("Executing SQL command.");
                    await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            getCatalog = new Catalog(
                                reader.GetString(0),
                                reader.GetTimeSpan(1),
                                reader.GetTimeSpan(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetInt16(5),
                                reader.GetInt32(6),
                                reader.GetInt32(7),
                                reader.GetInt32(8),
                                reader.GetString(9),
                                reader.GetInt32(10),
                                reader.GetInt16(11)
                            );
                        }
                    }
                }
                Log.Information("SQL command executed successfully.");
                return getCatalog;
            }
            catch (Exception e)
            {
                Log.Error("Error getting a row from the table by id.");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para puxar todos os dados da Tabela
        public async Task<List<Catalog>> GetsAsync()
        {
            try
            {
                Log.Information("Getting all Data from the table.");
                List<Catalog> listCatalog = new List<Catalog>();
                Catalog getCatalog;
                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        SELECT * FROM Catalog;
                    ";

                    Log.Information("Executing SQL command.");
                    await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            getCatalog = new Catalog(
                                reader.GetString(0),
                                reader.GetTimeSpan(1),
                                reader.GetTimeSpan(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetInt16(5),
                                reader.GetInt32(6),
                                reader.GetInt32(7),
                                reader.GetInt32(8),
                                reader.GetString(9),
                                reader.GetInt32(10),
                                reader.GetInt16(11)
                            );
                            listCatalog.Add(getCatalog);
                        }
                    }
                }
                Log.Information("SQL command executed successfully.");
                return listCatalog;
            }
            catch (Exception e)
            {
                Log.Error("Error getting all Data from the table.");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para atualizar uma linha da Tabela pelo ID
        public async Task UpdateAsync(string id, Catalog catalog)
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
                        UPDATE Catalog
                        SET updated_at = @UpdatedAt, title = @Title, author = @Author, year = @Year, rev = @Rev, publisher_id = @Publisher_Id, pages = @Pages, synopsis = @Synopsis, language_id = @Language_Id, is_foreign = @Is_Foreign WHERE id = @Id;
                    ";

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.TimeOfDay);
                    command.Parameters.AddWithValue("@Title", catalog.Title);
                    command.Parameters.AddWithValue("@Author", catalog.Author);
                    command.Parameters.AddWithValue("@Year", catalog.Year);
                    command.Parameters.AddWithValue("@Rev", catalog.Rev);
                    command.Parameters.AddWithValue("@Publisher_Id", catalog.Publisher_Id);
                    command.Parameters.AddWithValue("@Pages", catalog.Pages);
                    command.Parameters.AddWithValue("@Synopsis", catalog.Synopsis);
                    command.Parameters.AddWithValue("@Language_Id", catalog.Language_Id);
                    command.Parameters.AddWithValue("@Is_Foreign", catalog.Is_Foreign);

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
                        DELETE FROM Catalog
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