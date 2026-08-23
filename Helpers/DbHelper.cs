using CarServiceManager.Data;
using CarServiceManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CarServiceManager.Helpers
{
    public class DbHelper
    {
        private readonly CarServiceContext _context;

        public DbHelper(CarServiceContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message)> RegisterUserAsync(Users user)
        {
            try
            {
                var entity = _context.Model.FindEntityType(typeof(Users));

                var primaryKey = entity?.FindPrimaryKey();

                if (primaryKey == null)
                {
                    return (false, "EF Core does not see pkiUserID as the primary key.");
                }

                var existingUser = await _context.Users
                    .Where(u => u.userEmail == user.userEmail)
                    .FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    return (false, "User with this email already exists.");
                }

                user.hashPassword = HashPassword(user.hashPassword);
                user.isActive = true;
                user.isBlocked = false;
                user.dateCreated = DateTime.UtcNow;
                user.lastLogin = DateTime.UtcNow;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return (true, "User registered successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error registering user: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<Users?> LoginAsync(string email, string password)
        {
            string Hashedpassword = HashPassword(password);

            var user = await _context.Users
                .Where(u => 
                u.userEmail == email && 
                u.hashPassword == Hashedpassword &&
                u.isActive == true)
                .FirstOrDefaultAsync();

            return user;
        }

        private static string HashPassword(string password)
        {
            using SHA256 sha256 = SHA256.Create();

            byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new();

            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }

    }
}
