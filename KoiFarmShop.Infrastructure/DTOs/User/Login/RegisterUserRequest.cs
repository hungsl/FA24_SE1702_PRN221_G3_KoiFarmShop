namespace KoiFarmShop.Infrastructure.DTOs.User.Login
{
    public class RegisterUserRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Role { get; set; }
    }


    public class LoginUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
