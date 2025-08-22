using BibliotecaAPI.Data;
using Microsoft.Data.Sqlite;
using Serilog;

namespace BibliotecaAPI.Storages
{
    public class StorageLoan
    {
        private readonly string _connectionString;

        public StorageLoan(SQLiteDbConfig dbConfig)
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

        // Creates new loan
        public async Task AddLoanAsync(Loan loan)
        {
            try
            {
                Log.Information("Adding a new row to the table.");

                if (!string.IsNullOrEmpty(loan.Days_To_Expire))
                {
                    Log.Error("Invalid Loan Days To Expire.");
                    throw new ArgumentException("Invalid Loan Days To Expire.");
                }
                if (!string.IsNullOrEmpty(loan.Client_Id))
                {
                    Log.Error("Invalid Loan Client_Id.");
                    throw new ArgumentException("Invalid Loan Client_Id.");
                }
                if (!string.IsNullOrEmpty(loan.Inventory_Id))
                {
                    Log.Error("Invalid Loan Inventory_Id.");
                    throw new ArgumentException("Invalid Loan Inventory_Id.");
                }

                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        INSERT INTO Loan (id, created_at, updated_at, days_to_expire, returned_at, client_id, inventory_id) 
                        VALUES (@Id, @CreatedAt, @UpdatedAt, @Client_Id, @Inventory_Id, @Days_To_Expire, @Returned_At);
                    ";

                    command.Parameters.AddWithValue("@Id", Ulid.NewUlid().ToString());
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.TimeOfDay);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.TimeOfDay);
                    command.Parameters.AddWithValue("@Client_Id", loan.Client_Id);
                    command.Parameters.AddWithValue("@Inventory_Id", loan.Inventory_Id);
                    command.Parameters.AddWithValue("@Days_To_Expire", loan.Days_To_Expire);
                    command.Parameters.AddWithValue("@Returned_At", loan.Returned_At);

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

        // Return loan
        public async Task UpdateReturnAsync(string id, Loan loan)
        {
            try
            {
                // Log.Information("Method UpdateReturnAsync of ServiceLoan requested.");
                Log.Information("Updating a table row by Id.");

                if (!string.IsNullOrEmpty(loan.Days_To_Expire))
                {
                    Log.Error("Invalid Loan Days To Expire.");
                    throw new ArgumentException("Invalid Loan Days To Expire.");
                }

                if (!string.IsNullOrEmpty(loan.Returned_At.ToString()))
                {
                    Log.Error("Invalid Loan Returned At.");
                    throw new ArgumentException("Invalid Loan Returned At.");
                }

                if (!string.IsNullOrEmpty(id))
                {
                    Log.Error("Invalid Loan ID.");
                    throw new ArgumentException("Invalid Loan ID.");
                }

                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        UPDATE Loan
                        SET updated_at = updated_at = @UpdatedAt, client_id = @Client_Id, inventory_id = @Inventory_Id, days_to_expire = @Days_To_Expire, returned_at = @Returned_At WHERE id = @Id;
                    ";

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.TimeOfDay);
                    command.Parameters.AddWithValue("@Client_Id", loan.Client_Id);
                    command.Parameters.AddWithValue("@Inventory_Id", loan.Inventory_Id);
                    command.Parameters.AddWithValue("@Days_To_Expire", loan.Days_To_Expire);
                    command.Parameters.AddWithValue("@Returned_At", DateTime.Now.TimeOfDay);

                    Log.Information("Executing SQL command.");
                    await command.ExecuteNonQueryAsync();
                    Log.Information("SQL command executed successfully.");
                }
                // Log.Information("Method UpdateReturnAsync of ServiceLoan completed sucessfully.");
            }
            catch (Exception e)
            {
                // Log.Error($"Method UpdateReturnAsync of ServiceLoan request error: {e.Message}");
                Log.Error("Error updating a table row.");
                throw new Exception(e.Message);
            }
        }

        // Reports
        public async Task<List<Loan>> GetLateByBookIdAsync(string book_id)
        {
            try
            {
                // Log.Information("Method GetLateByBookIdAsync of ServiceLoan requested.");
                // var bookReport = await _storage.GetLateByBookIdAsync(id);
                // Log.Information("Method GetLateByBookIdAsync of ServiceLoan completed sucessfully.");
                // return bookReport;

                Log.Information("Getting a row from the table by id.");
                List<Loan> listLoans = new List<Loan>();
                Loan getLoan;
                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        SELECT * FROM Loan WHERE book_id = @Id;
                    ";
                    command.Parameters.AddWithValue("@Id", book_id);

                    Log.Information("Executing SQL command.");

                    await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            getLoan = new Loan(
                                reader.GetString(0),
                                reader.GetTimeSpan(1),
                                reader.GetTimeSpan(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetTimeSpan(6)
                            );
                            listLoans.Add(getLoan);
                        }
                    }
                }
                if (listLoans == null)
                {
                    Log.Error("Invalid Book Id.");
                    throw new ArgumentException("Invalid Book Id.");
                }
                Log.Information("SQL command executed successfully.");
                return listLoans;
            }
            catch (Exception e)
            {
                Log.Error($"Method GetLateByBookIdAsync of Storage request error: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Loan>> GetLateByClientId(string client_id)
        {
            try
            {
                // Log.Information("Method GetLateByClientId of ServiceLoan requested.");
                // var clientReport = await _storage.GetLateByClientId(id);
                // Log.Information("Method GetLateByClientId of ServiceLoan completed sucessfully.");
                // return clientReport;

                Log.Information("Getting a row from the table by id.");
                List<Loan> listLoans = new List<Loan>();
                Loan getLoan;
                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        SELECT * FROM Loan WHERE client_id = @Id;
                    ";
                    command.Parameters.AddWithValue("@Id", client_id);

                    Log.Information("Executing SQL command.");

                    await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            getLoan = new Loan(
                                reader.GetString(0),
                                reader.GetTimeSpan(1),
                                reader.GetTimeSpan(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetTimeSpan(6)
                            );
                            listLoans.Add(getLoan);
                        }
                    }
                }
                if (listLoans == null)
                {
                    Log.Error("Invalid Client Id.");
                    throw new ArgumentException("Invalid Client Id.");
                }
                Log.Information("SQL command executed successfully.");
                return listLoans;
            }
            catch (Exception e)
            {
                Log.Error($"Method GetLateByClientId of ServiceLoan request error: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Loan>> GetsLoansAsync()
        {
            try
            {
                Log.Information("Getting all Data from the table.");
                List<Loan> listLoans = new List<Loan>();
                Loan getLoan;
                await using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Log.Information("Opening a Database Connection.");
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        SELECT * FROM Loan;
                    ";

                    Log.Information("Executing SQL command.");

                    await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            getLoan = new Loan(
                                reader.GetString(0),
                                reader.GetTimeSpan(1),
                                reader.GetTimeSpan(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetTimeSpan(6)
                            );
                            listLoans.Add(getLoan);
                        }
                    }
                }
                if (listLoans == null)
                {
                    Log.Error("Invalid Client Id.");
                    throw new ArgumentException("Invalid Client Id.");
                }
                Log.Information("SQL command executed successfully.");
                return listLoans;            
            }
            catch (Exception e)
            {
                Log.Error($"Method GetsLoansAsync of ServiceLoan request error: {e.Message}");
                throw new Exception(e.Message);
            }
        }
    }
}