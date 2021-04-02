using System;
namespace mungmunglogServer.Models
{
    public class EmailJoinModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class EmailInfoJoinModel: EmailJoinModel
    {
        public string NickName { get; set; }
        public string Relationship { get; set; }
        public bool Gender { get; set; }
        public string FileUrl { get; set; }
    }

    public class JoinResponseModel: CommonResponse
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
