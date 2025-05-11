using Microsoft.EntityFrameworkCore;
using PasswordManager.Application.Interfaces;
using PasswordManager.Infrastructure.Data;
using PasswordManager.Infrastructure.Services;

namespace PasswordManager_API
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext for SQL Server
            services.AddDbContext<PasswordManagerContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Add application services
            services.AddScoped<IPasswordService, PasswordService>();

            // Add application Repository
            services.AddScoped<IPasswordRepository, PasswordRepository>();
        }
    }
}
