using Microsoft.EntityFrameworkCore;
using PasswordManager.Application.Interfaces;
using PasswordManager.Domain.Entities;
using PasswordManager.Infrastructure.Data;

public class PasswordRepository : IPasswordRepository
{
    private readonly PasswordManagerContext _context;

    public PasswordRepository(PasswordManagerContext context)
    {
        _context = context;
    }

    public async Task<List<PasswordManager.Domain.Entities.Password>> GetAllAsync()
    {
        return await _context.Passwords
            .Select(p => new PasswordManager.Domain.Entities.Password
            {
                Id = p.Id,
                Category = p.Category ?? "",
                App = p.App ?? "",
                UserName = p.UserName ?? "",
                EncryptedPassword = p.EncryptedPassword ?? ""
            })
            .ToListAsync();
    }

    public async Task<PasswordManager.Domain.Entities.Password?> GetById(int id)
    {
        var entity = await _context.Passwords.FindAsync(id);
        if (entity == null) return null;

        return new PasswordManager.Domain.Entities.Password
        {
            Id = entity.Id,
            Category = entity.Category ?? "",
            App = entity.App ?? "",
            UserName = entity.UserName ?? "",
            EncryptedPassword = entity.EncryptedPassword ?? ""
        };
    }

    public async Task<PasswordManager.Domain.Entities.Password?> GetByUserNameAsync(string userName)
    {
        var entity = await _context.Passwords
            .FirstOrDefaultAsync(p => p.UserName == userName);

        if (entity == null) return null;

        return new PasswordManager.Domain.Entities.Password
        {
            Id = entity.Id,
            Category = entity.Category ?? "",
            App = entity.App ?? "",
            UserName = entity.UserName ?? "",
            EncryptedPassword = entity.EncryptedPassword ?? ""
        };
    }

    public async Task AddAsync(PasswordManager.Domain.Entities.Password password)
    {
        var entity = new PasswordManager.Infrastructure.Data.Password
        {
            Category = password.Category,
            App = password.App,
            UserName = password.UserName,
            EncryptedPassword = password.EncryptedPassword
        };

        await _context.Passwords.AddAsync(entity);
    }

    public async Task UpdateAsync(PasswordManager.Domain.Entities.Password password)
    {
        var entity = await _context.Passwords.FindAsync(password.Id);
        if (entity != null)
        {
            entity.Category = password.Category;
            entity.App = password.App;
            entity.UserName = password.UserName;
            entity.EncryptedPassword = password.EncryptedPassword;
        }
    }

    public async Task DeleteAsync(PasswordManager.Domain.Entities.Password password)
    {
        var entity = await _context.Passwords.FindAsync(password.Id);
        if (entity != null)
        {
            _context.Passwords.Remove(entity);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
