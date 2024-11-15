using FluentValidation;
using Google.Apis.Auth;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DTOs.Common.Message;
using KoiFarmShop.Infrastructure.DTOs.User.Register;
using KoiFarmShop.Infrastructure.Interface;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Infrastructure.DTOs.User.Login;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        public AuthService(
            IUnitOfWork unitOfWork,
            IValidator<RegisterRequest> registerRequestValidator,
            IValidator<LoginRequest> loginRequestValidator,
            IPasswordHasher passwordHasher
            )
        {
            _unitOfWork = unitOfWork;
            _registerRequestValidator = registerRequestValidator;
            _loginRequestValidator = loginRequestValidator;
            _passwordHasher = passwordHasher;
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
