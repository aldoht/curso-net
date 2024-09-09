using Microsoft.AspNetCore.Mvc;
using Etapa4.Services;
using Etapa4.Data.BankDbModels;
namespace Etapa4.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase {
    private readonly AccountService _service;
    public AccountController(AccountService service) {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<Account> Get() {
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Account> GetById(int id) {
        var account = _service.GetById(id);

        if (account is null) return NotFound();
        return account;
    }

    [HttpPost]
    public IActionResult Create(Account account) {
        var newAccount = _service.Create(account);

        if (newAccount is null) return BadRequest();

        return CreatedAtAction(nameof(GetById), new {id = newAccount.Id}, newAccount);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Account account) {
        if (id != account.Id || account.Balance < 0) return BadRequest();

        var accountToUpdate = _service.GetById(id);

        if (accountToUpdate is null) return NotFound();

        int result = _service.Update(accountToUpdate);

        switch (result) {
            case -1:
                return NotFound();
            case 1:
                return BadRequest();
            default:
                return NoContent();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        var accountToDelete = _service.GetById(id);

        if (accountToDelete is null) return NotFound();

        _service.Delete(accountToDelete);
        return Ok();
    }
}