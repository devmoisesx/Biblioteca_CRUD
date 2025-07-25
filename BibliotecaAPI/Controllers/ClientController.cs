using BibliotecaAPI.Interface;
using BibliotecaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Data
{
    [ApiController]
    [Route("api/[controller]")]     // /api/client
    public class ClientController : ControllerBase
    {
        private readonly IServiceGeneric<Client> _serviceClient;

        public ClientController(IServiceGeneric<Client> serviceClient)
        {
            _serviceClient = serviceClient;
        }

        [HttpPost]
        public async Task<ActionResult<Client>> PostCliente(Client cliente)
        {
            try
            {
                await _serviceClient.AddAsync(cliente);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpGet("{id}")]       // /api/client/<id>
        public async Task<ActionResult<Client>> GetClientById(string id)
        {
            try
            {
                var client = await _serviceClient.GetByIdAsync(id);
                return Ok(client);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpGet]       // /api/client/
        public async Task<ActionResult<List<Client>>> GetClients()
        {
            try
            {
                List<Client> clients = await _serviceClient.GetsAsync();
                return Ok(clients);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpPut("{id}")]       // /api/client/<id>
        public async Task<ActionResult<Client>> UpdateClient(string id, Client client)
        {
            try
            {
                await _serviceClient.UpdateAsync(id, client);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }

        [HttpDelete("{id}")]        // /api/client/<id>
        public async Task<ActionResult<Client>> DeleteClient(string id)
        {
            try
            {
                await _serviceClient.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }
    }
}