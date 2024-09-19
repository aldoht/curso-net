using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Etapa4.Services;
using Etapa4.Data.BankDbModels;
using Etapa4.Data.DTOs;
namespace Etapa4.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase {
    private readonly AccountService _service;
    private readonly AccountTypeService accountTypeService;
    private readonly ClientService clientService;
    public AccountController(AccountService service, AccountTypeService accountTypeService, ClientService clientService) {
        this._service = service;
        this.accountTypeService = accountTypeService;
        this.clientService = clientService;
    }

    [HttpGet]
    public async Task<IEnumerable<Account>> Get() {
        return await _service.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetById(int id) {
        var account = await _service.GetById(id);

        if (account is null) return AccountNotFound(id);
        return account;
    }

    [HttpPost]
    public async Task<IActionResult> Create(AccountDTO account) {
        string validationResult = await ValidateAccount(account);

        if (!validationResult.Equals("Valid")) return BadRequest(new { message = validationResult });

        var newAccount = await _service.Create(account);
        return CreatedAtAction(nameof(GetById), new {id = newAccount.Id}, newAccount);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AccountDTO account) {
        if (id != account.Id) return differentIds(account.Id, id);
        if (account.Balance < 0) return BadRequest(new { message = $"Accounts must not have negative balances (new balance was {account.Balance})." });

        var accountToUpdate = await _service.GetById(id);

        if (accountToUpdate is null) return NotFound();

        int result = await _service.Update(account);

        switch (result) {
            case -1:
                return AccountNotFound(id);
            case 1:
                return differentIds(account.Id, id);
            default:
                return NoContent();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        var accountToDelete = await _service.GetById(id);

        if (accountToDelete is null) return AccountNotFound(id);

        await _service.Delete(accountToDelete);
        return Ok();
    }

    public NotFoundObjectResult AccountNotFound(int id) {
        return NotFound(new { message = $"Account with ID {id} does not exist." });
    }

    public BadRequestObjectResult differentIds(int accountId, int id) {
        return BadRequest(new { message = $"Account ID {accountId} is not the same as the URL's ID {id}." });
    }

    public async Task<string> ValidateAccount(AccountDTO account) {
        string result = "Valid";

        var accountType = await accountTypeService.GetById(account.AccountType);
        if (accountType is null) {
            result = $"Account's type does not exist.";
        }

        var clientId = account.ClientId.GetValueOrDefault();
        var client = await clientService.GetById(clientId);

        if (client is null) {
            result = $"Client with ID {clientId} does not exist.";
        }

        if (account.Balance < 0) {
            result = $"Account balance must not be negative (tried with {account.Balance}).";
        }

        return result;
    }
}