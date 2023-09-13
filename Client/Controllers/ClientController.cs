using Client.Filters;
using ClientNamespace.Model;
using ClientNamespace.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ClientNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [ClientExceptionFilter]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
           _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients(int page = 1, int pageSize = 25)
        {
            var clients = await _clientRepository.GetAllClientsAsync();

            if (clients != null || clients.Count != 0)
            {
                var totalCount = clients.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var productsPerPage = clients
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                return Ok(productsPerPage);
            }
            return NotFound("No Records Found");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Please Enter correct Id");
            }
            var client = await _clientRepository.GetClientByIdAsync(id);

            if (client == null)
            {
                return NotFound($"No record Found with the {id}");
            }
            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewClient([FromBody] ClientModel clientModel)
        {
            if (clientModel == null)
            {
                return BadRequest("Please add The Data of client");
            }

            var id = await _clientRepository.AddClientAsync(clientModel); ;
            return CreatedAtAction(nameof(GetClientById), new { id, controller = "client" }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient([FromBody] ClientModel clientModel, [FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Please Enter correct Id");
            }
            var client = await _clientRepository.GetClientByIdAsync(id);

            if (client == null)
            {
                return NotFound($"No record Found with the {id}");
            }

            await _clientRepository.UpdateClientAsync(id, clientModel);
            return Ok("Updated");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateClientPatch([FromBody] JsonPatchDocument clientModel, [FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Please Enter correct Id");
            }
            var client = await _clientRepository.GetClientByIdAsync(id);

            if (client == null)
            {
                return NotFound($"No record Found with the {id}");
            }

            await _clientRepository.UpdateClientPatchAsync(id, clientModel);
            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Please Enter correct Id");
            }
            var client = await _clientRepository.GetClientByIdAsync(id);

            if (client == null)
            {
                return NotFound($"No record Found with the {id}");
            }

            await _clientRepository.DeleteClientAsync(id);
            return Ok($"Client with {id} deleted");
        }
    }
}
