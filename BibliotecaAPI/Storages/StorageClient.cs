namespace BibliotecaAPI.Storages
{
    public class StorageClient
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public StorageClient()
        {
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

    }
}