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
        private readonly IServiceGeneric<Book> _serviceCatalog;

        public CatalogController(IServiceGeneric<Book> serviceCatalog)
        {
            _serviceCatalog = serviceCatalog;
        }

        [HttpPost] 
        public async Task<ActionResult<Book>> PostCatalogAsync(Book catalog)
        {
            try
            {
                Log.Information("Post Book request Initialized.");
                await _serviceCatalog.AddAsync(catalog);
                Log.Information("Post Book request completed successfully");
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error($"Post Book request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("Post Book request completed.");
            }
        }

        [HttpGet("{id}")]       // /api/catalog/<id>
        public async Task<ActionResult<Book>> GetCatalogByIdAsync(string id)
        {
            try
            {
                Log.Information("Get Book By Id request Initialized.");
                var catalog = await _serviceCatalog.GetByIdAsync(id);
                Log.Information("Get Book By Id request completed successfully");
                return Ok(catalog);
            }
            catch (Exception e)
            {
                Log.Error($"Get Book By Id request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("Get Book By Id request completed.");
            }
        }

        [HttpGet]       // /api/catalog/
        public async Task<ActionResult<List<Book>>> GetCatalogsAsync()
        {
            try
            {
                Log.Information("Get Book request Initialized.");
                List<Book> catalogs = await _serviceCatalog.GetsAsync();
                Log.Information("Get Book request completed successfully.");
                return Ok(catalogs);
            }
            catch (Exception e)
            {
                Log.Error($"Get Book request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("ReGet Book requestquest completed.");
            }
        }

        [HttpPut("{id}")]       // /api/catalog/<id>
        public async Task<ActionResult<Book>> UpdateCatalogAsync(string id, Book catalog)
        {
            try
            {
                Log.Information("Update Book request Initialized.");
                await _serviceCatalog.UpdateAsync(id, catalog);
                Log.Information("Update Book request completed successfully.");
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error($"Update Book request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("Update Book request completed.");
            }
        }

        [HttpDelete("{id}")]        // /api/catalog/<id>
        public async Task<ActionResult<Book>> DeleteCatalogAsync(string id)
        {
            try
            {
                Log.Information("Delete Book request Initialized.");
                await _serviceCatalog.DeleteAsync(id);
                Log.Information("Delete Book request completed successfully.");
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error($"Delete Book request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("Delete Book request completed.");
            }
        }
    }
}
