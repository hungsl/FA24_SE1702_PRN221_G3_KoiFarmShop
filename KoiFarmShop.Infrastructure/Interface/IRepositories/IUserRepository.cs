using KoiFarmShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.Interface.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> UserNameExistsAsync(string userName);
        Task<bool> EmailExistsAsync(string email);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<User> GetUserByIdAsync(Guid id);
    }
}
