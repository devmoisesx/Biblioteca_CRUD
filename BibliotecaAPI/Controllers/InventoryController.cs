using BibliotecaAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BibliotecaAPI.Data
{
    [ApiController]
    [Route("api/[controller]")]     // /api/inventory
    public class InventoryController : ControllerBase
    {
        private readonly IServiceGeneric<Inventory> _serviceInventory;

        private static readonly Serilog.ILogger log = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs.txt")
            .CreateLogger();

        public InventoryController(IServiceGeneric<Inventory> serviceInventory)
        {
            _serviceInventory = serviceInventory;
        }

        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventoryAsync(Inventory inventory)
        {
            try
            {
                log.Information("Post Inventory request Initialized.");
                await _serviceInventory.AddAsync(inventory);
                log.Information("Post Inventory request completed successfully");
                return Ok();
            }
            catch (Exception e)
            {
                log.Error($"Post Inventory request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("Post Inventory request completed.");
            }
        }

        [HttpGet("{id}")]       // /api/inventory/<id>
        public async Task<ActionResult<Inventory>> GetInventoryByIdAsync(string id)
        {
            try
            {
                log.Information("Get Inventory By Id request Initialized.");
                var inventory = await _serviceInventory.GetByIdAsync(id);
                log.Information("Get Inventory By Id request completed successfully");
                return Ok(inventory);
            }
            catch (Exception e)
            {
                log.Error($"Get Inventory By Id request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("Get Inventory By Id request completed.");
            }
        }

        [HttpGet]       // /api/inventory/
        public async Task<ActionResult<List<Inventory>>> GetInventorysAsync()
        {
            try
            {
                log.Information("Get Inventorys request Initialized.");
                List<Inventory> inventorys = await _serviceInventory.GetsAsync();
                log.Information("Get Inventorys request completed successfully");
                return Ok(inventorys);
            }
            catch (Exception e)
            {
                log.Error($"Get Inventorys request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("Get Inventorys request completed.");
            }
        }

        [HttpPut("{id}")]       // /api/inventory/<id>
        public async Task<ActionResult<Inventory>> UpdateInventoryAsync(string id, Inventory inventory)
        {
            try
            {
                log.Information("Update Inventory request Initialized.");
                await _serviceInventory.UpdateAsync(id, inventory);
                log.Information("Update Inventory request completed successfully");
                return Ok();
            }
            catch (Exception e)
            {
                log.Error($"Update Inventory request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("Update Inventory request completed.");
            }
        }

        [HttpDelete("{id}")]        // /api/inventory/<id>
        public async Task<ActionResult<Inventory>> DeleteInventoryAsync(string id)
        {
            try
            {
                log.Information("Delete Inventory request Initialized.");
                await _serviceInventory.DeleteAsync(id);
                log.Information("Delete Inventory request completed successfully");
                return Ok();
            }
            catch (Exception e)
            {
                log.Error($"Delete Inventory request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("Delete Inventory request completed.");
            }
        }
    }
}
