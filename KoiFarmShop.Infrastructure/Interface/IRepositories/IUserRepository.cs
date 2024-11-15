using KoiFarmShop.Domain.Entities;
<<<<<<< HEAD
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> Dev_Danh_skibidi

namespace KoiFarmShop.Infrastructure.Interface.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> UserNameExistsAsync(string userName);
        Task<bool> EmailExistsAsync(string email);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
<<<<<<< HEAD

        Task<User> RegisterUserAsync(User user);
        public Task<User> LoginUserAsync(string username, string password);
        Task<User> GetUserByIdAsync(Guid id);
=======
>>>>>>> Dev_Danh_skibidi
    }
}
