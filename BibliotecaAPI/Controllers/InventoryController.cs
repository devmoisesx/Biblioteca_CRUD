using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaAPI.Interface;
using BibliotecaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Data
{
    [ApiController]
    [Route("api/[controller]")]
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
                await _serviceInventory.AddAsync(inventory);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventoryByIdAsync(string id)
        {
            try
            {
                var inventory = await _serviceInventory.GetByIdAsync(id);
                return Ok(inventory);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Inventory>>> GetInventorysAsync()
        {
            try
            {
                List<Inventory> inventorys = await _serviceInventory.GetsAsync();
                return Ok(inventorys);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Inventory>> UpdateInventoryAsync(string id, Inventory inventory)
        {
            try
            {
                await _serviceInventory.UpdateAsync(id, inventory);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Inventory>> DeleteInventoryAsync(string id)
        {
            try
            {
                await _serviceInventory.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }
    }
}