using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Infrastructure.DTOs.User.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.RazorWebApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(IAuthService authService, ILogger<RegisterModel> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [BindProperty]
        public RegisterUserRequest RegisterRequest { get; set; }

        // Remove the BindProperty attribute from ErrorMessage
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Remove validation for ProfilePictureUrl if it’s not required
            ModelState.Remove("RegisterRequest.ProfilePictureUrl");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning("ModelState error: {Error}", error.ErrorMessage);
                }
                return Page();
            }

            _logger.LogInformation("Attempting to register user with email: {Email}", RegisterRequest.Email);

            var result = await _authService.RegisterUserAsync(RegisterRequest);

            if (!result.IsSuccess)
            {
                ErrorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("User registration failed: {Errors}", ErrorMessage);
                return Page();
            }

            _logger.LogInformation("User registered successfully with email: {Email}", RegisterRequest.Email);
            return RedirectToPage("/Account/Login");
        }

    }

}
