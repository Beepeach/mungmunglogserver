using System;
namespace mungmunglogServer.Models
{
    public class EmailLoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseModel: CommonResponse
    {
        public string Email { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Nickname { get; set; }
        public int? FamilyId { get; set; }
    }

    public class SNSLoginRequestModel
    {
        public string Provider { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
