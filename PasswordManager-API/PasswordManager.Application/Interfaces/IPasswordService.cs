using PasswordManager.API.Dtos;
using PasswordManager.Domain.Entities;

namespace PasswordManager.Application.Interfaces
{
    public interface IPasswordService
    {
        Task<List<Password>> GetAllAsync();
        Task<Password?> GetByIdAsync(int id);
        Task<Password?> GetByUserNameAsync(string userName);
        Task<Password> AddAsync(PasswordDto dto);
        Task<Password?> UpdateAsync(int id, PasswordDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
