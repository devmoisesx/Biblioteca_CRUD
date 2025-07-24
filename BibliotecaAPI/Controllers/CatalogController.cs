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
    public class CatalogController : ControllerBase
    {
        private readonly IServiceGeneric<Catalog> _serviceCatalog;

        public CatalogController(IServiceGeneric<Catalog> serviceCatalog)
        {
            _serviceCatalog = serviceCatalog;
        }

        [HttpPost]
        public async Task<ActionResult<Catalog>> PostCatalogAsync(Catalog catalog)
        {
            try
            {
                await _serviceCatalog.AddAsync(catalog);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Catalog>> GetCatalogByIdAsync(string id)
        {
            try
            {
                var catalog = await _serviceCatalog.GetByIdAsync(id);
                return Ok(catalog);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Catalog>>> GetCatalogsAsync()
        {
            try
            {
                List<Catalog> catalogs = await _serviceCatalog.GetsAsync();
                return Ok(catalogs);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Catalog>> UpdateCatalogAsync(string id, Catalog catalog)
        {
            try
            {
                await _serviceCatalog.UpdateAsync(id, catalog);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Catalog>> DeleteCatalogAsync(string id)
        {
            try
            {
                await _serviceCatalog.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }
    }
}