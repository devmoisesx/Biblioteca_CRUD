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
    public class ClientController : ControllerBase
    {
        private readonly IServiceClient _serviceClient;

        public ClientController(IServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        [HttpPost]
        public ActionResult<Client> PostCliente(Client cliente)
        {
            try
            {
                _serviceClient.AddClient(cliente);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao criar transacao: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Client> GetClientById(string id)
        {
            try
            {
                var client = _serviceClient.GetClientById(id);
                return Ok(client);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erro interno: {e.Message}");
            }
        }

        [HttpGet]
        public ActionResult<List<Client>> GetClients()
        {
            try
            {
                List<Client> clients = _serviceClient.GetClients();
                return Ok(clients);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erro interno: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Client> UpdateClient(string id, Client client)
        {
            try
            {
                _serviceClient.UpdateClient(id, client);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao criar transacao: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Client> DeleteClient(string id)
        {
            try
            {
                _serviceClient.DeleteClient(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao criar transacao: {e.Message}");
            }
        }
    }
}