using Microsoft.Data.Sqlite;
using Serilog;

namespace BibliotecaAPI.Data
{
    public class SQLiteDbConfig
    {
        private readonly IConfiguration _configuration;
        private static string _connectionString;

        private static readonly Serilog.ILogger log = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs.txt")
            .CreateLogger();

        public SQLiteDbConfig(IConfiguration configuration)
        {
            _configuration = configuration;
            string rawConnectionString = _configuration.GetConnectionString("DefaultConnection");       // puxa do arquivo appsettings.json a string para conexao ao SQLite

            if (string.IsNullOrEmpty(rawConnectionString))
            {
                throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada em appsettings.json.");
            }

            // Verifica se a string de conexao possui Data Source=, se não possuir, adiciona 
            if (!rawConnectionString.Contains("Data Source="))
            {
                string dbFileName = rawConnectionString;
                string dbPath = Path.Combine(AppContext.BaseDirectory, dbFileName);
                _connectionString = $"Data Source={dbPath}";
            }
            else
            {
                _connectionString = rawConnectionString;
            }
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        // Inicia do Db
        public void InitializeDatabase()
        {
            log.Information("Start database.");
            using (var connection = new SqliteConnection(GetConnectionString()))
            {
                connection.Open();
                var command = connection.CreateCommand();

                using (var pragmaCommand = new SqliteCommand("PRAGMA foreign_keys = ON;", connection))      // ativa as Foreign Keys no SQLite
                {
                    pragmaCommand.ExecuteNonQuery();
                }

                // Cria a tabela Client
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Client (
                        id char(26) PRIMARY KEY,
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                        name varchar NOT NULL,
                        email varchar NOT NULL,
                        phone char(14)
                    );
                ";
                command.ExecuteNonQuery();

                // Cria a tabela Book
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Book (
                        id char(13) PRIMARY KEY,
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 

                        title varchar NOT NULL,
                        author varchar NOT NULL,
                        year INTEGER NOT NULL,
                        rev INTEGER NOT NULL,
                        pages INTEGER NOT NULL,
                        synopsis varchar ,
                        is_foreign INTEGER NOT NULL DEFAULT 0,
                        publisher_id INTEGER,
                        language_id INTEGER,
                        FOREIGN KEY (publisher_id) REFERENCES Publisher(id),
                        FOREIGN KEY (language_id) REFERENCES Language(id)
                    );
                ";
                command.ExecuteNonQuery();

                // Cria a tabela Genrer
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Genrer (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
                        
                        value varchar NOT NULL
                    );
                ";
                command.ExecuteNonQuery();

                // Cria a tabela BookGenre
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS BookGenre (
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        book_id char(13),
                        genrer_id INTEGER,
                        FOREIGN KEY (book_id) REFERENCES Book(id),
                        FOREIGN KEY (genrer_id) REFERENCES Genrer(id)
                    );
                    
                ";
                command.ExecuteNonQuery();

                // Cria a tabela Publisher
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Publisher (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                        name varchar NOT NULL
                    );
                    
                ";
                command.ExecuteNonQuery();

                // Cria a tabela Language
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Language (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 

                        name varchar NOT NULL
                    );
                    
                ";
                command.ExecuteNonQuery();

                // Cria a tabela Item
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Item (
                        id char(13) PRIMARY KEY,
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 

                        rented INTEGER NOT NULL,
                        returned INTEGER,
                        date_rented TEXT NOT NULL,
                        rate_returned TEXT,
                        book_id varchar NOT NULL,
                        client_id varchar NOT NULL,
                        FOREIGN KEY (book_id) REFERENCES Book(id),
                        FOREIGN KEY (client_id) REFERENCES Client(id)
                    );
                ";
                command.ExecuteNonQuery();

                // Cria a tabela Inventory
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Inventory (
                        id char(26) PRIMARY KEY,
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                        condition INTEGER NOT NULL DEFAULT 100,
                        is_available INTEGER NOT NULL DEFAULT 1,
                        book_id char(13),
                        FOREIGN KEY (book_id) REFERENCES Book(id)
                    );
                    
                ";
                command.ExecuteNonQuery();

                // Cria a tabela Loan
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Loan (
                        id char(26) PRIMARY KEY,
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                        days_to_expire INTEGER NOT NULL DEFAULT 30,
                        returned_at TIMESTAMP DEFAULT NULL,
                        client_id char(26),
                        inventory_id char(26),
                        FOREIGN KEY (client_id) REFERENCES Client(id),
                        FOREIGN KEY (inventory_id) REFERENCES Inventory(id)
                    );
                    
                ";
                command.ExecuteNonQuery();
            }

        }
    }
}
