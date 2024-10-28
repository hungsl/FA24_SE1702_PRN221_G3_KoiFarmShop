using FluentValidation;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Infrastructure.DTOs.Common.Message;
using KoiFarmShop.Infrastructure.DTOs.User.Login;
using KoiFarmShop.Infrastructure.DTOs.User.Register;
using KoiFarmShop.Infrastructure.Interface;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using Microsoft.Extensions.Configuration;

namespace KoiFarmShop.Application.Implement.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RegisterRequest> _registerRequestValidator;
        private readonly IValidator<LoginRequest> _loginRequestValidator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;


        public AuthService(
            IUnitOfWork unitOfWork,
            IValidator<RegisterRequest> registerRequestValidator,
            IValidator<LoginRequest> loginRequestValidator,
            IPasswordHasher passwordHasher
,
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _registerRequestValidator = registerRequestValidator;
            _loginRequestValidator = loginRequestValidator;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        // Register a new user
        public async Task<Result> RegisterUserAsync(RegisterUserRequest request)
        {
            // Map request to User entity
            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Username = request.Username,
                PasswordHash = request.Password, // Password will be hashed in the repository
                DateOfBirth = request.DateOfBirth,
                role = request.Role
            };

            try
            {
                // Attempt to register the user
                var registeredUser = await _userRepository.RegisterUserAsync(user);
                return Result.SuccessWithObject(new { UserId = registeredUser.Id });
            }
            catch (InvalidOperationException ex)
            {
                // Handle existing user conflicts
                return Result.Failure(Error.Conflict("UserExists", ex.Message));
            }
        }

        // Login user
        public async Task<Result> LoginUserAsync(LoginUserRequest request)
        {
            // Attempt to find and verify the user
            var user = await _userRepository.LoginUserAsync(request.Username, request.Password);

            if (user == null)
            {
                // Return a failure result if login details are incorrect
                return Result.Failure(Error.Validation("InvalidLogin", "Invalid username or password."));
            }

            // Return success with user information
            var userInfo = new { UserId = user.Id, user.Username, user.role };
            return Result.SuccessWithObject(userInfo);
        }




        public async Task<Result> SignIn(LoginRequest loginRequest)
        {
            var validate = await _loginRequestValidator.ValidateAsync(loginRequest);
            if (!validate.IsValid)
            {
                var errors = validate.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();

                // Handle errors as needed, e.g., return them in a Result object
                return Result.Failures(errors);
            }
            var userLogin = await _unitOfWork.UserRepository.GetByAsync("Email", loginRequest.Email); // fixxxxxxxxxx
            var checkPassword = _passwordHasher.VerifyPassword(loginRequest.Password, userLogin.PasswordHash);
            if (userLogin == null || checkPassword == false)
            {
                return Result.Failure(UserErrorMessage.UserNotExist());
            }

            var loginResponse = new LoginResponse
            {
                ReNewToken = "Test",
                AccessToken = "Test",
            };

            return Result.SuccessWithObject(loginResponse);

        }

        public async Task<Result> SignUp(RegisterRequest registerRequest)
        {
            var validate = await _registerRequestValidator.ValidateAsync(registerRequest);
            if (!validate.IsValid)
            {
                var errors = validate.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();
                return Result.Failures(errors);
            }
            User newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = registerRequest.Email,
                PasswordHash = _passwordHasher.HashPassword(registerRequest.Password),
                Username = registerRequest.UserName

            };
            var createUsre = await _unitOfWork.UserRepository.CreateAsync(newUser);
            if (createUsre == 0)
            {
                return Result.Failure(UserErrorMessage.UserNoCreated());
            }
            return Result.SuccessWithObject(newUser);
        }
        //public string GenerateJwtToken(string email, int Role, double expirationMinutes)//them tham so role de phan quyen
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    // add role to author
        //    var role = Role switch
        //    {
        //        1 => "Admin",
        //        2 => "Customer",
        //        3 => "Staff",
        //        4 => "Manager"
        //    };
        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, email),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(ClaimTypes.Role, role)// claim role
        //    };

        //    var token = new JwtSecurityToken(
        //        issuer: _configuration["Jwt:Issuer"],
        //        audience: _configuration["Jwt:Audience"],
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(expirationMinutes),
        //        signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        //public async Task<User> FindOrCreateUser(GoogleJsonWebSignature.Payload payload)
        //{
        //    var user = await _unitOfWork.UserRepository.GetByAsync("Email", payload.Email);
        //    if (user == null)
        //    {
        //        user = new User
        //        {
        //            Id = Guid.NewGuid(),
        //            Email = payload.Email,
        //            FullName = payload.Name,
        //        };
        //        await _unitOfWork.UserRepository.CreateAsync(user);
        //    }
        //    return user;
        //}


    }
}
