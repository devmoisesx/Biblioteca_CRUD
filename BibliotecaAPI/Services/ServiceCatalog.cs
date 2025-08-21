using BibliotecaAPI.Interface;
using BibliotecaAPI.Storages;
using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;

namespace BibliotecaAPI.Services
{
    public class ServiceCatalog : IServiceGeneric<Book>
    {
        private readonly StorageCatalog _storage;

        public ServiceCatalog(StorageCatalog storage)
        {
            try
            {
                Log.Information("Creating a Storage instance.");
                _storage = storage;     // Cria uma instancia do Storage
                Log.Information("Storage instance created successfully.");
            }
            catch (Exception e)
            {
                Log.Error("Error creating Storage instance.");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para adicionar um novo Book
        public async Task AddAsync(Book book)
        {
            try
            {
                Log.Information("Method AddAsync of ServiceCatalog requested.");
                await _storage.AddAsync(book);
                Log.Information("Method AddAsync of ServiceCatalog completed sucessfully.");
            }
            catch (Exception e)
            {
                Log.Error($"Method AddAsync of ServiceCatalog request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para puxar do Db o Book especifico pelo Id
        public async Task<Book> GetByIdAsync(string id)
        {
            try
            {
                Log.Information("Method GetByIdAsync of ServiceCatalog requested.");
                var catalogId = await _storage.GetByIdAsync(id);
                Log.Information("Method GetByIdAsync of ServiceCatalog completed sucessfully.");
                return catalogId;
            }
            catch (Exception e)
            {
                Log.Error($"Method GetByIdAsync of ServiceCatalog request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para puxar do Db todos os Catalogs
        public async Task<List<Book>> GetsAsync()
        {
            try
            {
                Log.Information("Method GetsAsync of ServiceCatalog requested.");
                var catalogData = await _storage.GetsAsync();
                Log.Information("Method GetsAsync of ServiceCatalog completed sucessfully.");
                return catalogData;            
            }
            catch (Exception e)
            {
                Log.Error($"Method GetsAsync of ServiceCatalog request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para atualizar uma Linha da tabela Book
        public async Task UpdateAsync(string id, Book book)
        {
            try
            {
                Log.Information("Method UpdateAsync of ServiceCatalog requested.");
                await _storage.UpdateAsync(id, book);
                Log.Information("Method UpdateAsync of ServiceCatalog completed sucessfully.");
            }
            catch (Exception e)
            {
                Log.Error($"Method UpdateAsync of ServiceCatalog request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para deletar uma Linha da tabela Book pelo id
        public async Task DeleteAsync(string id)
        {
            try
            {
                Log.Information("Method DeleteAsync of ServiceCatalog requested.");
                await _storage.DeleteAsync(id);
                Log.Information("Method DeleteAsync of ServiceCatalog completed sucessfully.");
            }
            catch (Exception e)
            {
                Log.Error($"Method DeleteAsync of ServiceCatalog request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
        }
    }
}