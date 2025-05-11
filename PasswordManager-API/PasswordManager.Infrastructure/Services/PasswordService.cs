using PasswordManager.API.Dtos;
using PasswordManager.Application.Interfaces;
using PasswordManager.Domain.Entities;
using PasswordManager.Infrastructure.Helper;

namespace PasswordManager.Infrastructure.Services
{

    public class PasswordService : IPasswordService
    {
        private readonly IPasswordRepository _repository;

        public PasswordService(IPasswordRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Password>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Password?> GetByIdAsync(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Password?> GetByUserNameAsync(string userName)
        {
            return await _repository.GetByUserNameAsync(userName);
        }

        public async Task<Password> AddAsync(PasswordDto dto)
        {
            var password = new Password
            {
                Category = dto.Category,
                App = dto.App,
                UserName = dto.UserName,
                EncryptedPassword = Base64Helper.Encrypt(dto.Password)
            };

            await _repository.AddAsync(password);
            await _repository.SaveChangesAsync();

            return password;
        }

        public async Task<Password?> UpdateAsync(int id, PasswordDto dto)
        {
            var existing = await _repository.GetById(id);
            if (existing == null) return null;

            existing.Category = dto.Category;
            existing.App = dto.App;
            existing.UserName = dto.UserName;
            existing.EncryptedPassword = Base64Helper.Encrypt(dto.Password);

            await _repository.UpdateAsync(existing);
            await _repository.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetById(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(existing);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
