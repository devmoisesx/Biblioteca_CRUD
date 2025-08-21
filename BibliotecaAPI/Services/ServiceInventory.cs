using BibliotecaAPI.Interface;
using BibliotecaAPI.Storages;
using Serilog;

namespace BibliotecaAPI.Services
{
    public class ServiceInventory : IServiceGeneric<Inventory>
    {
        private readonly StorageInventory _storage;

        public ServiceInventory(StorageInventory storage)
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
                throw new Exception(e.Message);
            }
        }

        // Metodo para adicionar um novo Inventory
        public async Task AddAsync(Inventory inventory)
        {
            try
            {
                Log.Information("Method AddAsync of ServiceInventory requested.");
                await _storage.AddAsync(inventory);
                Log.Information("Method AddAsync of ServiceInventory completed sucessfully.");
            }
            catch (Exception e)
            {
                Log.Error($"Method AddAsync of ServiceInventory request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para puxar do Db o Inventory especifico pelo Id
        public async Task<Inventory> GetByIdAsync(string id)
        {
            try
            {
                Log.Information("Method GetByIdAsync of ServiceInventory requested.");
                var inventoryId = await _storage.GetByIdAsync(id);
                Log.Information("Method GetByIdAsync of ServiceInventory completed sucessfully.");
                return inventoryId;
            }
            catch (Exception e)
            {
                Log.Error($"Method GetByIdAsync of ServiceInventory request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
        }

        // Metodo para puxar do Db todos os Inventory
        public async Task<List<Inventory>> GetsAsync()
        {
            try
            {
                Log.Information("Method GetsAsync of ServiceInventory requested.");
                var inventorys = await _storage.GetsAsync();
                Log.Information("Method GetsAsync of ServiceInventory completed sucessfully.");
                return inventorys;
            }
            catch (Exception e)
            {
                Log.Error($"Method GetsAsync of ServiceInventory request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
            
        }

        // Metodo para atualizar uma Linha da tabela Inventory
        public async Task UpdateAsync(string id, Inventory inventory)
        {
            try
            {
                Log.Information("Method UpdateAsync of ServiceInventory requested.");
                await _storage.UpdateAsync(id, inventory);
                Log.Information("Method UpdateAsync of ServiceInventory completed sucessfully.");
            }
            catch (Exception e)
            {
                Log.Error($"Method UpdateAsync of ServiceInventory request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
            
        }

        // Metodo para deletar uma Linha da tabela Inventory pelo id
        public async Task DeleteAsync(string id)
        {
            try
            {
                Log.Information("Method DeleteAsync of ServiceInventory requested.");
                await _storage.DeleteAsync(id);
                Log.Information("Method DeleteAsync of ServiceInventory completed sucessfully.");
            }
            catch (Exception e)
            {
                Log.Error($"Method DeleteAsync of ServiceInventory request error: {e.Message}");
                throw new Exception($"Erro: {e.Message}");
            }
            
        }
    }
}