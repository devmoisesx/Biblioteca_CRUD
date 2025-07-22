using Microsoft.Data.Sqlite;

namespace BibliotecaAPI.Data
{
    public class SQLiteDbConfig
    {
        private readonly IConfiguration _configuration;
        private static string _connectionString;

        public SQLiteDbConfig(IConfiguration configuration)
        {
            _configuration = configuration;
            string rawConnectionString = _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(rawConnectionString))
            {
                throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada em appsettings.json.");
            }

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

        public static string GetConnectionString()
        {
            return _connectionString;
        }

        public void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(GetConnectionString()))
            {
                connection.Open();
                var command = connection.CreateCommand();

                using (var pragmaCommand = new SqliteCommand("PRAGMA foreign_keys = ON;", connection))
                {
                    pragmaCommand.ExecuteNonQuery();
                }

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

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Catalog (
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

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Genrer (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
                        
                        value varchar NOT NULL
                    );
                ";
                command.ExecuteNonQuery();

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS CatalogGenre (
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        catalog_id char(13),
                        genrer_id INTEGER,
                        FOREIGN KEY (catalog_id) REFERENCES Catalog(id),
                        FOREIGN KEY (genrer_id) REFERENCES Genrer(id)
                    );
                    
                ";
                command.ExecuteNonQuery();

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Publisher (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                        name varchar NOT NULL
                    );
                    
                ";
                command.ExecuteNonQuery();

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Language (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 

                        name varchar NOT NULL
                    );
                    
                ";
                command.ExecuteNonQuery();

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Inventory (
                        id char(26) PRIMARY KEY,
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                        condition INTEGER NOT NULL DEFAULT 100,
                        is_available INTEGER NOT NULL DEFAULT 1,
                        catalog_id char(13),
                        FOREIGN KEY (catalog_id) REFERENCES Catalog(id)
                    );
                    
                ";
                command.ExecuteNonQuery();

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