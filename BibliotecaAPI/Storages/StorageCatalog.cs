using BibliotecaAPI.Data;
using BibliotecaAPI.Interface.Storages;
using Microsoft.Data.Sqlite;

namespace BibliotecaAPI.Storages
{
    public class StorageCatalog : IStorageGeneric<Catalog>
    {
        private readonly string _connectionString;

        public StorageCatalog(SQLiteDbConfig dbConfig)
        {
            _connectionString = dbConfig.GetConnectionString();     // Puxa a string de conexao do Db
        }

        // Metodo para Adicionar uma nova linha na Tabela
        public async Task AddAsync(Catalog catalog)
        {
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
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

                command.ExecuteNonQuery();
            }
        }

        // Metodo para puxar uma linha da Tabela pelo ID
        public async Task<Catalog> GetByIdAsync(string id)
        {
            Catalog getCatalog = null;
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    SELECT * FROM Catalog WHERE id = @Id;
                ";

                command.Parameters.AddWithValue("@Id", id);

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
            return getCatalog;
        }

        // Metodo para puxar todos os dados da Tabela
        public async Task<List<Catalog>> GetsAsync()
        {
            List<Catalog> listCatalog = new List<Catalog>();
            Catalog getCatalog;
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    SELECT * FROM Catalog;
                ";

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
            return listCatalog;
        }

        // Metodo para atualizar uma linha da Tabela pelo ID
        public async Task UpdateAsync(string id, Catalog catalog)
        {
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
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

                await command.ExecuteNonQueryAsync();
            }
        }

        // Metodo para Deletar uma linha da Tabela pelo ID
        public async Task DeleteAsync(string id)
        {
            await using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    DELETE FROM Catalog
                    WHERE id = @Id;
                ";

                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}