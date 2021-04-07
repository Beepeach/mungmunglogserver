using System;
using System.Collections.Generic;

namespace mungmunglogServer.Models
{
    public static class StatusCode
    {
        public const int Ok = 200;
        public const int NotFound = 404;
        public const int Unknown = -999;
        public const int Fail = -998;
        public const int FailWithDuplication = -997;
    }

    public class CommonResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    // 한가지를 리턴하는 Response
    public class SingleResponse<T>: CommonResponse
    {
        public T Data { get; set; }
    }

    // list를 리턴하는 Response
    public class ListResponse<T>: CommonResponse
    {
        public List<T> List { get; set; }
    }
}
