using System;
namespace mungmunglogServer.Models
{
    public static class StatusCode
    {
        public const int Ok = 200;
        public const int NotFound = 404;
        public const int Unknowb = -999;
        public const int Fail = -998;
    }

    public class CommonResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
