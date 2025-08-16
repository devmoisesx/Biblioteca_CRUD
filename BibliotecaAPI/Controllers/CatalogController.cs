using BibliotecaAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BibliotecaAPI.Data
{
    [ApiController]
    [Route("api/[controller]")]
    // localhost/api/catalog
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
                Log.Information("Post Catalog request Initialized.");
                await _serviceCatalog.AddAsync(catalog);
                Log.Information("Post Catalog request completed successfully");
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error($"Post Catalog request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                Log.Information("Post Catalog request completed.");
            }
        }

        [HttpGet("{id}")]       // /api/catalog/<id>
        public async Task<ActionResult<Catalog>> GetCatalogByIdAsync(string id)
        {
            try
            {
                Log.Information("Get Catalog By Id request Initialized.");
                var catalog = await _serviceCatalog.GetByIdAsync(id);
                Log.Information("Get Catalog By Id request completed successfully");
                return Ok(catalog);
            }
            catch (Exception e)
            {
                Log.Error($"Get Catalog By Id request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                Log.Information("Get Catalog By Id request completed.");
            }
        }

        [HttpGet]       // /api/catalog/
        public async Task<ActionResult<List<Catalog>>> GetCatalogsAsync()
        {
            try
            {
                Log.Information("Get Catalog request Initialized.");
                List<Catalog> catalogs = await _serviceCatalog.GetsAsync();
                Log.Information("Get Catalog request completed successfully.");
                return Ok(catalogs);
            }
            catch (Exception e)
            {
                Log.Error($"Get Catalog request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                Log.Information("ReGet Catalog requestquest completed.");
            }
        }

        [HttpPut("{id}")]       // /api/catalog/<id>
        public async Task<ActionResult<Catalog>> UpdateCatalogAsync(string id, Catalog catalog)
        {
            try
            {
                Log.Information("Update Catalog request Initialized.");
                await _serviceCatalog.UpdateAsync(id, catalog);
                Log.Information("Update Catalog request completed successfully.");
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error($"Update Catalog request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                Log.Information("Update Catalog request completed.");
            }
        }

        [HttpDelete("{id}")]        // /api/catalog/<id>
        public async Task<ActionResult<Catalog>> DeleteCatalogAsync(string id)
        {
            try
            {
                Log.Information("Delete Catalog request Initialized.");
                await _serviceCatalog.DeleteAsync(id);
                Log.Information("Delete Catalog request completed successfully.");
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error($"Delete Catalog request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                Log.Information("Delete Catalog request completed.");
            }
        }
    }
}
