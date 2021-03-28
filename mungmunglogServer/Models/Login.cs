using System;
namespace mungmunglogServer.Models
{
    public class EmailLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseModel: CommonResponse
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
