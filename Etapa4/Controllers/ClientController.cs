using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Etapa4.Services;
using Etapa4.Data.BankDbModels;
namespace Etapa4.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase {
    private readonly ClientService _service;
    public ClientController(ClientService service) {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<Client>> Get() {
        return await _service.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetById(int id) {
        var client = await _service.GetById(id);

        if (client is null) return ClientNotFound(id);
        return client;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Client client) {
        var newClient = await _service.Create(client);

        if (newClient is null) return BadRequest();

        return CreatedAtAction(nameof(GetById), new {id = newClient.Id}, newClient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Client client) {
        if (id != client.Id) return differentIds(client.Id, id);

        var clientToUpdate = await _service.GetById(id);

        if (clientToUpdate is null) return NotFound();

        await _service.Update(clientToUpdate);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        var ClientToDelete = await _service.GetById(id);

        if (ClientToDelete is null) return ClientNotFound(id);

        await _service.Delete(id);
        return Ok();
    }

    public NotFoundObjectResult ClientNotFound(int id) {
        return NotFound(new { message = $"Client with ID {id} does not exist." });
    }

    public BadRequestObjectResult differentIds(int clientId, int id) {
        return BadRequest(new { message = $"Client ID {clientId} is not the same as the URL's ID {id}." });
    }
}