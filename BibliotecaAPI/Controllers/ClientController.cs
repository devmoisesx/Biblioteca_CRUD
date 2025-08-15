using BibliotecaAPI.Interface;
using BibliotecaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BibliotecaAPI.Data
{
    [ApiController]
    [Route("api/[controller]")]     // /api/client
    public class ClientController : ControllerBase
    {
        private readonly IServiceGeneric<Client> _serviceClient;

        private static readonly Serilog.ILogger log = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs.txt")
            .CreateLogger();

        public ClientController(IServiceGeneric<Client> serviceClient)
        {
            _serviceClient = serviceClient;
        }

        [HttpPost]
        public async Task<ActionResult<Client>> PostCliente(Client cliente)
        {
            try
            {
                log.Information("Post Client request Initialized.");
                await _serviceClient.AddAsync(cliente);
                log.Information("Post Client request completed successfully");
                return Ok();
            }
            catch (Exception e)
            {
                log.Error($"Post Client request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("Request completed.");
            }
        }

        [HttpGet("{id}")]       // /api/client/<id>
        public async Task<ActionResult<Client>> GetClientById(string id)
        {
            try
            {
                log.Information("Get Client By Id request Initialized.");
                var client = await _serviceClient.GetByIdAsync(id);
                log.Information("Get Client By Id request completed successfully");
                return Ok(client);
            }
            catch (Exception e)
            {
                log.Error($"Get Client By Id request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("Request completed.");
            }
        }

        [HttpGet]       // /api/client/
        public async Task<ActionResult<List<Client>>> GetClients()
        {
            try
            {
                log.Information("Get Clients request Initialized.");
                List<Client> clients = await _serviceClient.GetsAsync();
                log.Information("Get Clients request completed successfully.");
                return Ok(clients);
            }
            catch (Exception e)
            {
                log.Error($"Get Clients request error: {e.Message}.");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("Request completed.");
            }
        }

        [HttpPut("{id}")]       // /api/client/<id>
        public async Task<ActionResult<Client>> UpdateClient(string id, Client client)
        {
            try
            {
                log.Information("Update Client request Initialized.");
                await _serviceClient.UpdateAsync(id, client);
                log.Information("Update Client request completed successfully.");
                return Ok();
            }
            catch (Exception e)
            {
                log.Error($"Update Client request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("Request completed.");
            }
        }

        [HttpDelete("{id}")]        // /api/client/<id>
        public async Task<ActionResult<Client>> DeleteClient(string id)
        {
            try
            {
                log.Information("Delete Client request Initialized.");
                await _serviceClient.DeleteAsync(id);
                log.Information("Delete Client request completed successfully.");
                return Ok();
            }
            catch (Exception e)
            {
                log.Error($"Delete Client request error: {e.Message}");
                return BadRequest($"Erro: {e.Message}");
            }
            finally
            {
                log.Information("Request completed.");
            }
        }
    }
}
