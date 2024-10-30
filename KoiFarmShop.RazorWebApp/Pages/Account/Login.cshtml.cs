using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Infrastructure.DTOs.User.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.RazorWebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(IAuthService authService, ILogger<LoginModel> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [BindProperty]
        public LoginUserRequest LoginRequest { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid for login attempt.");
                return Page();
            }

            _logger.LogInformation("Attempting login for username: {Username}", LoginRequest.Username);

            var result = await _authService.LoginUserAsync(LoginRequest);
            if (!result.IsSuccess)
            {
                ErrorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Login failed for username: {Username}. Error: {ErrorMessage}", LoginRequest.Username, ErrorMessage);
                return Page();
            }

            _logger.LogInformation("User logged in successfully: {Username}", LoginRequest.Username);

            // Add authentication logic here if needed, e.g., set cookies

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Account/Login");
        }
    }
}
