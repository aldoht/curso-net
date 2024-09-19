using Microsoft.EntityFrameworkCore;
using Etapa4.Data;
using Etapa4.Data.BankDbModels;

namespace Etapa4.Services;

public class AccountTypeService {
    private readonly BankDbContext _context;
    public AccountTypeService(BankDbContext context) {
        _context = context;
    }

    public async Task<AccountType?> GetById(int id) {
        return await _context.AccountTypes.FindAsync(id);
    }
}