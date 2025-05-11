using PasswordManager.Domain.Entities;

public interface IPasswordRepository
{
    Task<List<Password>> GetAllAsync();
    Task<Password?> GetById(int id);
    Task<Password?> GetByUserNameAsync(string userName);
    Task AddAsync(Password password);
    Task UpdateAsync(Password password);
    Task DeleteAsync(Password password);
    Task SaveChangesAsync();
}