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

        public InventoryController(IServiceGeneric<Inventory> serviceInventory)
        {
            _serviceInventory = serviceInventory;
        }

        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventoryAsync(Inventory inventory)
        {
            try
            {
                Log.Information("Post Inventory request Initialized.");
                await _serviceInventory.AddAsync(inventory);
                Log.Information("Post Inventory request completed successfully");
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error($"Post Inventory request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("Post Inventory request completed.");
            }
        }

        [HttpGet("{id}")]       // /api/inventory/<id>
        public async Task<ActionResult<Inventory>> GetInventoryByIdAsync(string id)
        {
            try
            {
                Log.Information("Get Inventory By Id request Initialized.");
                var inventory = await _serviceInventory.GetByIdAsync(id);
                Log.Information("Get Inventory By Id request completed successfully");
                return Ok(inventory);
            }
            catch (Exception e)
            {
                Log.Error($"Get Inventory By Id request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("Get Inventory By Id request completed.");
            }
        }

        [HttpGet]       // /api/inventory/
        public async Task<ActionResult<List<Inventory>>> GetInventorysAsync()
        {
            try
            {
                Log.Information("Get Inventorys request Initialized.");
                List<Inventory> inventorys = await _serviceInventory.GetsAsync();
                Log.Information("Get Inventorys request completed successfully");
                return Ok(inventorys);
            }
            catch (Exception e)
            {
                Log.Error($"Get Inventorys request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("Get Inventorys request completed.");
            }
        }

        [HttpPut("{id}")]       // /api/inventory/<id>
        public async Task<ActionResult<Inventory>> UpdateInventoryAsync(string id, Inventory inventory)
        {
            try
            {
                Log.Information("Update Inventory request Initialized.");
                await _serviceInventory.UpdateAsync(id, inventory);
                Log.Information("Update Inventory request completed successfully");
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error($"Update Inventory request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("Update Inventory request completed.");
            }
        }

        [HttpDelete("{id}")]        // /api/inventory/<id>
        public async Task<ActionResult<Inventory>> DeleteInventoryAsync(string id)
        {
            try
            {
                Log.Information("Delete Inventory request Initialized.");
                await _serviceInventory.DeleteAsync(id);
                Log.Information("Delete Inventory request completed successfully");
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error($"Delete Inventory request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("Delete Inventory request completed.");
            }
        }
    }
}
