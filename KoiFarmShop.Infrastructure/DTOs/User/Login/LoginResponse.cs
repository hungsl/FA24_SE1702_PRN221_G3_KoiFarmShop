using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.DTOs.User.Login
{
    public class LoginResponse
    {
        public string ReNewToken { get; set; }
        public string AccessToken { get; set; }
    }
}
