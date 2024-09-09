using Etapa4.Data;
using Etapa4.Data.BankDbModels;

namespace Etapa4.Services;

public class AccountService {
    private readonly BankDbContext _context;
    public AccountService(BankDbContext context) {
        _context = context;
    }

    public IEnumerable<Account> GetAll() {
        return _context.Accounts.ToList();
    }

    public Account? GetById(int id) {
        var account = _context.Accounts.Find(id);

        return account;
    }

    public Account Create(Account newAccount) {
        if (_context.Clients.Find(newAccount.ClientId) is null) return null;
        _context.Accounts.Add(newAccount);
        _context.SaveChanges();
        return newAccount;
    }

    public int Update(Account account) {
        var existingAccount = GetById(account.Id);
        if (existingAccount is null) return -1;

        if (existingAccount.ClientId != account.ClientId) return 1;

        existingAccount.AccountType = account.AccountType;
        existingAccount.Balance = account.Balance;

        _context.SaveChanges();
        return 0;
    }

    public void Delete(Account account) {
        var accountToDelete = GetById(account.Id);

        if (accountToDelete is null) return;

        _context.Accounts.Remove(accountToDelete);
        _context.SaveChanges();
    }
}