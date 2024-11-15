using FluentValidation;
using Google.Apis.Auth;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Infrastructure.DTOs.Common.Message;
using KoiFarmShop.Infrastructure.DTOs.User.Login;
using KoiFarmShop.Infrastructure.DTOs.User.Register;
using KoiFarmShop.Infrastructure.Interface;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
        private readonly ILogger<AuthService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;




        public AuthService(
            IUnitOfWork unitOfWork,
            IValidator<RegisterRequest> registerRequestValidator,
            IValidator<LoginRequest> loginRequestValidator,
            IPasswordHasher passwordHasher
,
            IUserRepository userRepository,
            IConfiguration configuration,
            ILogger<AuthService> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _registerRequestValidator = registerRequestValidator;
            _loginRequestValidator = loginRequestValidator;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result> RegisterUserAsync(RegisterUserRequest request)
        {
            _logger.LogInformation("Starting user registration for email: {Email}", request.Email);

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Username = request.Username,
                PasswordHash = request.Password, // Password will be hashed in the repository
                DateOfBirth = request.DateOfBirth,
                role = request.Role,
                ProfilePictureUrl = request.ProfilePictureUrl ?? string.Empty // Set default if null
            };

            try
            {
                var registeredUser = await _userRepository.RegisterUserAsync(user);
                _logger.LogInformation("User successfully registered with email: {Email}", request.Email);
                return Result.SuccessWithObject(new { UserId = registeredUser.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during user registration.");
                return Result.Failure(Error.Failure("UnexpectedError", "An unexpected error occurred. Please try again later."));
            }
        }


        // Login user
        public async Task<Result> LoginUserAsync(LoginUserRequest request)
        {
            _logger.LogInformation("Starting login process for username: {Username}", request.Username);

            try
            {
                // Attempt to find and verify the user
                var user = await _userRepository.LoginUserAsync(request.Username, request.Password);

                if (user == null)
                {
                    _logger.LogWarning("Login failed: Invalid username or password for username: {Username}", request.Username);
                    return Result.Failure(Error.Validation("InvalidLogin", "Invalid username or password."));
                }

                _logger.LogInformation("Login successful for username: {Username}", request.Username);

                // Set up claims for the user
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.role.ToString())
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Keeps the user logged in across sessions
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) // Set a session timeout, if desired
                };

                // Use HttpContext from IHttpContextAccessor to SignIn
                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // Return success with user information
                var userInfo = new { UserId = user.Id, user.Username, user.role };
                return Result.SuccessWithObject(userInfo);
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "An error occurred during login for username: {Username}", request.Username);

                // Return a general failure result
                return Result.Failure(Error.Failure("LoginError", "An error occurred during login. Please try again later."));
            }
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
