using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> Dev_Danh_skibidi

namespace KoiFarmShop.Infrastructure.Implement.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(KVSCContext context) : base(context) { }

        #region bool
        public async Task<bool> UserNameExistsAsync(string userName)
        {
            // Check if any user exists with the specified username
            return await _context.Users.AnyAsync(x => x.Username == userName);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            // Check if any user exists with the specified email
            return await _context.Users.AnyAsync(x => x.Email == email);
        }
        #endregion

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email) && x.PasswordHash.Equals(password));
        }

<<<<<<< HEAD
        // Register a new user
        public async Task<User> RegisterUserAsync(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username || u.Email == user.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with the same username or email already exists.");
            }

            user.PasswordHash = HashPassword(user.PasswordHash); // Assuming plain text in PasswordHash is used for simplicity
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Login user
        public async Task<User> LoginUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return null; // Invalid login attempt
            }
            return user;
        }

        // Password hashing
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password); // Replace with actual hashing implementation
        }

        // Password verification
        private bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash); // Replace with actual verification implementation
        }
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users
                .Where(u => u.Id == id && !u.IsDeleted)
                .FirstOrDefaultAsync();
        }
=======

>>>>>>> Dev_Danh_skibidi



    }
}
