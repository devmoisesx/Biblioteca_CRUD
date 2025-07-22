using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace BibliotecaAPI.Data
{
    public static class SQLiteDbConfig
    {
        private static string _connectionString;
        private static readonly string DbFileName = "Biblioteca.db";

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                string dbPath = Path.Combine(AppContext.BaseDirectory, DbFileName);
                _connectionString = $"Data Source={dbPath}";
            }
            return _connectionString;
        }

        public static void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(GetConnectionString()))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Client (
                        id char(26) [primary key]
                        created_at timestamp [default: now()]
                        updated_at timestamp [default: now()]

                        name varchar [not null]
                        email varchar [not null]
                        phone char(14)
                    )
                ";
                command.ExecuteNonQuery();

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Catalog (
                    id char(13) [primary key] // ISBN
                    created_at timestamp [default: now()]
                    updated_at timestamp [default: now()] 

                    title varchar [not null]
                    author varchar [not null]
                    year int [not null]
                    rev int [not null]
                    publisher_id int [not null]
                    pages int [not null]
                    synopsis varchar 
                    language_id int [not null]
                    is_foreign bool [not null, default: false]
                    )
                ";
                command.ExecuteNonQuery();

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Genrer (
                    id int [primary key, increment]
                    created_at timestamp [default: now()]
                    updated_at timestamp [default: now()] 
                    
                    value varchar
                    )
                ";
                command.ExecuteNonQuery();

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS CatalogGenre (
                    catalog_id char(13)
                    genre_id int
                    created_at timestamp [default: now()]
                    updated_at timestamp [default: now()]
                    )
                    Ref: Catalog.id > CatalogGenre.catalog_id
                    Ref: Genrer.id > CatalogGenre.genre_id

                ";
                command.ExecuteNonQuery();

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Publisher (
                    id int [primary key, increment]
                    created_at timestamp [default: now()]
                    updated_at timestamp [default: now()]

                    name varchar [not null]   
                    )
                    Ref: Publisher.id - Catalog.publisher_id

                ";
                command.ExecuteNonQuery();

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Language (
                    id int [primary key, increment]
                    created_at timestamp [default: now()]
                    updated_at timestamp [default: now()] 

                    name varchar [not null]  
                    )
                    Ref: Language.id - Catalog.language_id

                ";
                command.ExecuteNonQuery();

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Inventory (
                    id char(26) [primary key]
                    created_at timestamp [default: now()]
                    updated_at timestamp [default: now()]

                    catalog_id char(13)  
                    condition int [not null, default: 100]
                    is_available bool [not null, default: true]
                    )
                    Ref: Inventory.catalog_id > Catalog.id

                ";
                command.ExecuteNonQuery();

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Loan (
                    id char(26) [primary key]
                    created_at timestamp [default: now()]
                    updated_at timestamp [default: now()]

                    client_id char(26) [not null]
                    inventory_id char(26) [not null]
                    days_to_expire int [not null, default: 30]
                    returned_at timestamp [default: null]
                    )
                    Ref: Client.id < Loan.client_id
                    Ref: Inventory.id < Loan.inventory_id
                ";
                command.ExecuteNonQuery();
            }

        }
    }
}