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

        
        private static readonly Serilog.ILogger log = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs.txt")
            .CreateLogger();

        public CatalogController(IServiceGeneric<Catalog> serviceCatalog)
        {
            _serviceCatalog = serviceCatalog;
        }

        [HttpPost] 
        public async Task<ActionResult<Catalog>> PostCatalogAsync(Catalog catalog)
        {
            try
            {
                log.Information("Post Catalog request Initialized.");
                await _serviceCatalog.AddAsync(catalog);
                log.Information("Post Catalog request completed successfully");
                return Ok();
            }
            catch (Exception e)
            {
                log.Error($"Post Catalog request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("Post Catalog request completed.");
            }
        }

        [HttpGet("{id}")]       // /api/catalog/<id>
        public async Task<ActionResult<Catalog>> GetCatalogByIdAsync(string id)
        {
            try
            {
                log.Information("Get Catalog By Id request Initialized.");
                var catalog = await _serviceCatalog.GetByIdAsync(id);
                log.Information("Get Catalog By Id request completed successfully");
                return Ok(catalog);
            }
            catch (Exception e)
            {
                log.Error($"Get Catalog By Id request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("Get Catalog By Id request completed.");
            }
        }

        [HttpGet]       // /api/catalog/
        public async Task<ActionResult<List<Catalog>>> GetCatalogsAsync()
        {
            try
            {
                log.Information("Get Catalog request Initialized.");
                List<Catalog> catalogs = await _serviceCatalog.GetsAsync();
                log.Information("Get Catalog request completed successfully.");
                return Ok(catalogs);
            }
            catch (Exception e)
            {
                log.Error($"Get Catalog request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("ReGet Catalog requestquest completed.");
            }
        }

        [HttpPut("{id}")]       // /api/catalog/<id>
        public async Task<ActionResult<Catalog>> UpdateCatalogAsync(string id, Catalog catalog)
        {
            try
            {
                log.Information("Update Catalog request Initialized.");
                await _serviceCatalog.UpdateAsync(id, catalog);
                log.Information("Update Catalog request completed successfully.");
                return Ok();
            }
            catch (Exception e)
            {
                log.Error($"Update Catalog request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("Update Catalog request completed.");
            }
        }

        [HttpDelete("{id}")]        // /api/catalog/<id>
        public async Task<ActionResult<Catalog>> DeleteCatalogAsync(string id)
        {
            try
            {
                log.Information("Delete Catalog request Initialized.");
                await _serviceCatalog.DeleteAsync(id);
                log.Information("Delete Catalog request completed successfully.");
                return Ok();
            }
            catch (Exception e)
            {
                log.Error($"Delete Catalog request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("Delete Catalog request completed.");
            }
        }
    }
}
