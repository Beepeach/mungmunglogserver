using System;
namespace mungmunglogServer.Models
{
    public class EmailJoinRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class JoinInfoRequestModel
    {
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Relationship { get; set; }
        public bool Gender { get; set; }
        public string FileUrl { get; set; }
    }

    public class JoinResponseModel: CommonResponse
    {
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
