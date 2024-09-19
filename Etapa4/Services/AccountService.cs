using Microsoft.EntityFrameworkCore;
using Etapa4.Data;
using Etapa4.Data.BankDbModels;
using Etapa4.Data.DTOs;

namespace Etapa4.Services;

public class AccountService {
    private readonly BankDbContext _context;
    public AccountService(BankDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Account>> GetAll() {
        return await _context.Accounts.ToListAsync();
    }

    public async Task<Account?> GetById(int id) {
        return await _context.Accounts.FindAsync(id);
    }

    public async Task<Account> Create(AccountDTO newAccountDTO) {
        var newAccount = new Account();
        newAccount.AccountType = newAccountDTO.AccountType;
        newAccount.ClientId = newAccountDTO.ClientId;
        newAccount.Balance = newAccountDTO.Balance;

        if (await _context.Clients.FindAsync(newAccount.ClientId) is null) return null;

        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync();
        return newAccount;
    }

    public async Task<int> Update(AccountDTO account) {
        var existingAccount = await GetById(account.Id);
        if (existingAccount is null) return -1;

        if (existingAccount.ClientId != account.ClientId) return 1;

        existingAccount.AccountType = account.AccountType;
        existingAccount.Balance = account.Balance;

        await _context.SaveChangesAsync();
        return 0;
    }

    public async Task Delete(Account account) {
        var accountToDelete = await GetById(account.Id);

        if (accountToDelete is null) return;

        _context.Accounts.Remove(accountToDelete);
        await _context.SaveChangesAsync();
    }
}