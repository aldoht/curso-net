using Microsoft.EntityFrameworkCore;
using Etapa4.Data;
using Etapa4.Data.BankDbModels;

namespace Etapa4.Services;

public class ClientService {
    private readonly BankDbContext _context;
    public ClientService(BankDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAll() {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetById(int id) {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<Client> Create(Client newClient) {
        _context.Clients.Add(newClient);
        await _context.SaveChangesAsync();
        return newClient;
    }

    public async Task Update(Client client) {
        var existingClient = await GetById(client.Id);
        if (existingClient is null) return;

        existingClient.Name = client.Name;
        existingClient.PhoneNumber = client.PhoneNumber;
        existingClient.Email = client.Email;

        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id) {
        var ClientToDelete = await GetById(id);

        if (ClientToDelete is null) return;

        _context.Clients.Remove(ClientToDelete);
        await _context.SaveChangesAsync();
    }
}