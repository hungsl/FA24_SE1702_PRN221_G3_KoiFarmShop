using KoiFarmShop.Domain.Entities;

namespace KoiFarmShop.Infrastructure.Interface.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> UserNameExistsAsync(string userName);
        Task<bool> EmailExistsAsync(string email);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);

        Task<User> RegisterUserAsync(User user);
        public Task<User> LoginUserAsync(string username, string password);
        Task<User> GetUserByIdAsync(Guid id);
    }
}
