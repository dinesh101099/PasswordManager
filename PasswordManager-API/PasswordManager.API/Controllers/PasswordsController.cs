using Microsoft.AspNetCore.Mvc;
using PasswordManager.API.Dtos;
using PasswordManager.Application.Interfaces;
using PasswordManager.Infrastructure.Helper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PasswordManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordsController : ControllerBase
    {
        private readonly IPasswordService _service;

        public PasswordsController(IPasswordService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPasswordRecordAsync([FromQuery] bool decrypt = false)
        {
            var results = await _service.GetAllAsync();

            if (decrypt)
            {
                var decryptedResults = results.Select(result => new
                {
                    result.Id,
                    result.Category,
                    result.App,
                    result.UserName,
                    DecryptedPassword = Base64Helper.Decrypt(result.EncryptedPassword)
                });

                return Ok(decryptedResults);
            }

            return Ok(results);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPasswordRecordByIdAsync(int id, [FromQuery] bool decrypt = false)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();

            if (decrypt)
            {
                return Ok(new
                {
                    result.Id,
                    result.Category,
                    result.App,
                    result.UserName,
                    DecryptedPassword = Base64Helper.Decrypt(result.EncryptedPassword)
                });
            }

            return Ok(result);
        }

        [HttpGet("by-username/{userName}")]
        public async Task<IActionResult> GetPasswordRecordByUserNameAsync(string userName, [FromQuery] bool decrypt = false)
        {
            var result = await _service.GetByUserNameAsync(userName);
            if (result == null) return NotFound();

            if (decrypt)
            {
                return Ok(new
                {
                    result.Id,
                    result.Category,
                    result.App,
                    result.UserName,
                    DecryptedPassword = Base64Helper.Decrypt(result.EncryptedPassword)
                });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePasswordRecordAsync(PasswordDto dto)
        {
            var created = await _service.AddAsync(dto);
            return created == null ? NotFound() : Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePasswordRecordByIdAsync(int id, PasswordDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePasswordRecordByIdAsync(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
