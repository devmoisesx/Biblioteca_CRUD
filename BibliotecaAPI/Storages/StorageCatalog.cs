using BibliotecaAPI.Data;
using BibliotecaAPI.Interface.Storages;
using Microsoft.Data.Sqlite;
using Serilog;

namespace BibliotecaAPI.Storages
{
    public class StorageCatalog : IStorageGeneric<Book>
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
                throw new Exception(e.Message);
            }
        }

        // Metodo para Adicionar uma nova linha na Tabela
        public async Task AddAsync(Book book)
        {
            try
            {
                Log.Information("Adding a new row to the table.");

                if (!string.IsNullOrEmpty(book.Title) && char.IsDigit(book.Title[0]))
                {
                    Log.Error("Invalid Title.");
                    throw new ArgumentException("Invalid Book Title.");
                }

                if (!string.IsNullOrEmpty(book.Author) && char.IsDigit(book.Author[0]))
                {
                    Log.Error("Invalid Book Author.");
                    throw new ArgumentException("Invalid Book Author.");         
                }

                if (!int.IsEvenInteger(book.Year))
                {
                    Log.Error("Invalid Year.");
                    throw new ArgumentException("Invalid Book Year.");
                }

                if (!int.IsEvenInteger(book.Rev))
                {
                    Log.Error("Invalid Rev.");
                    throw new ArgumentException("Invalid Book Rev.");
                }

                if (!int.IsEvenInteger(book.Pages))
                {
                    Log.Error("Invalid Pages.");
                    throw new ArgumentException("Invalid Book Pages.");
                }

                if (!string.IsNullOrEmpty(book.Synopsis) && char.IsDigit(book.Synopsis[0]))
                {
                    Log.Error("Invalid Book Synopsis.");
                    throw new ArgumentException("Invalid Book Synopsis.");         
                }

                if (!int.IsEvenInteger(book.Is_Foreign))
                {
                    Log.Error("Invalid Is Foreign.");
                    throw new ArgumentException("Invalid Book Is Foreign.");
                }

                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        INSERT INTO Book (id, created_at, updated_at, title, author, year, rev, publisher_id, pages, synopsis, language_id, is_foreign) 
                        VALUES (@Id, @CreatedAt, @UpdatedAt, @Title, @Author, @Year, @Rev, @Publisher_Id, @Pages, @Synopsis, @Language_Id, @Is_Foreign);
                    ";

                    command.Parameters.AddWithValue("@Id", Ulid.NewUlid().ToString());
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.TimeOfDay);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.TimeOfDay);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@Year", book.Year);
                    command.Parameters.AddWithValue("@Rev", book.Rev);
                    command.Parameters.AddWithValue("@Publisher_Id", book.Publisher_Id);
                    command.Parameters.AddWithValue("@Pages", book.Pages);
                    command.Parameters.AddWithValue("@Synopsis", book.Synopsis);
                    command.Parameters.AddWithValue("@Language_Id", book.Language_Id);
                    command.Parameters.AddWithValue("@Is_Foreign", book.Is_Foreign);

                    Log.Information("Executing SQL command in the Database.");
                    command.ExecuteNonQuery();
                }
                Log.Information("SQL command executed successfully.");
            }
            catch (Exception e)
            {
                Log.Error("Error adding a new row to the table.");
                throw new Exception(e.Message);
            }
        }

        // Metodo para puxar uma linha da Tabela pelo ID
        public async Task<Book> GetByIdAsync(string id)
        {
            try
            {
                Log.Information("Getting a row from the table by id.");
                Book getCatalog = null;
                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        SELECT * FROM Book WHERE id = @Id;
                    ";

                    command.Parameters.AddWithValue("@Id", id);

                    Log.Information("Executing SQL command.");
                    await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            getCatalog = new Book(
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
                if (getCatalog == null)
                {
                    Log.Error("Invalid Book Id.");
                    throw new ArgumentException("Invalid Book Id.");
                }
                Log.Information("SQL command executed successfully.");
                return getCatalog;
            }
            catch (Exception e)
            {
                Log.Error("Error getting a row from the table by id.");
                throw new Exception(e.Message);
            }
        }

        // Metodo para puxar todos os dados da Tabela
        public async Task<List<Book>> GetsAsync()
        {
            try
            {
                Log.Information("Getting all Data from the table.");
                List<Book> listCatalog = new List<Book>();
                Book getCatalog;
                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        SELECT * FROM Book;
                    ";

                    Log.Information("Executing SQL command.");
                    await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            getCatalog = new Book(
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
                throw new Exception(e.Message);
            }
        }

        // Metodo para atualizar uma linha da Tabela pelo ID
        public async Task UpdateAsync(string id, Book book)
        {
            try
            {
                Log.Information("Updating a table row by Id.");

                if (!string.IsNullOrEmpty(book.Title) && char.IsDigit(book.Title[0]))
                {
                    Log.Error("Invalid Title.");
                    throw new ArgumentException("Invalid Book Title.");
                }

                if (!string.IsNullOrEmpty(book.Author) && char.IsDigit(book.Author[0]))
                {
                    Log.Error("Invalid Book Author.");
                    throw new ArgumentException("Invalid Book Author.");         
                }

                if (!int.IsEvenInteger(book.Year))
                {
                    Log.Error("Invalid Year.");
                    throw new ArgumentException("Invalid Book Year.");
                }

                if (!int.IsEvenInteger(book.Rev))
                {
                    Log.Error("Invalid Rev.");
                    throw new ArgumentException("Invalid Book Rev.");
                }

                if (!int.IsEvenInteger(book.Pages))
                {
                    Log.Error("Invalid Pages.");
                    throw new ArgumentException("Invalid Book Pages.");
                }

                if (!string.IsNullOrEmpty(book.Synopsis) && char.IsDigit(book.Synopsis[0]))
                {
                    Log.Error("Invalid Book Synopsis.");
                    throw new ArgumentException("Invalid Book Synopsis.");         
                }

                if (!int.IsEvenInteger(book.Is_Foreign))
                {
                    Log.Error("Invalid Is Foreign.");
                    throw new ArgumentException("Invalid Book Is Foreign.");
                }

                if (!string.IsNullOrEmpty(id))
                {
                    Log.Error("Invalid Book ID.");
                    throw new ArgumentException("Invalid Book ID.");
                }

                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        UPDATE Book
                        SET updated_at = @UpdatedAt, title = @Title, author = @Author, year = @Year, rev = @Rev, publisher_id = @Publisher_Id, pages = @Pages, synopsis = @Synopsis, language_id = @Language_Id, is_foreign = @Is_Foreign WHERE id = @Id;
                    ";

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.TimeOfDay);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@Year", book.Year);
                    command.Parameters.AddWithValue("@Rev", book.Rev);
                    command.Parameters.AddWithValue("@Publisher_Id", book.Publisher_Id);
                    command.Parameters.AddWithValue("@Pages", book.Pages);
                    command.Parameters.AddWithValue("@Synopsis", book.Synopsis);
                    command.Parameters.AddWithValue("@Language_Id", book.Language_Id);
                    command.Parameters.AddWithValue("@Is_Foreign", book.Is_Foreign);

                    Log.Information("Executing SQL command.");
                    await command.ExecuteNonQueryAsync();
                    Log.Information("SQL command executed successfully.");
                }
            }
            catch (Exception e)
            {
                Log.Error("Error updating a table row by Id.");
                throw new Exception(e.Message);
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
                    Log.Error("Invalid Book ID.");
                    throw new ArgumentException("Invalid Book ID.");
                }

                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        DELETE FROM Book
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
                throw new Exception(e.Message);
            }
        }
    }
}