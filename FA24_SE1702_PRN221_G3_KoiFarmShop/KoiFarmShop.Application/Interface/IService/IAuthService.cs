using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Infrastructure.DTOs.User.Login;
using KoiFarmShop.Infrastructure.DTOs.User.Register;

namespace KoiFarmShop.Application.Interface.IService
{
    public interface IAuthService
    {
        Task<Result> RegisterUserAsync(RegisterUserRequest request);
        public Task<Result> LoginUserAsync(LoginUserRequest request);

        Task<Result> SignIn(LoginRequest loginRequest);
        Task<Result> SignUp(RegisterRequest registerRequest);
        //public string GenerateJwtToken(string email, int Role, double expirationMinutes);
        //public Task<User> FindOrCreateUser(GoogleJsonWebSignature.Payload payload);

    }
}
