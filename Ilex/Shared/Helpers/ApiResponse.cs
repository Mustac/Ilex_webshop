using System.Net;

namespace Ilex.Shared.Helpers
{
    public class ApiResponse<T>
    {
        public T Content { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public HttpStatusCode ResponseCode { get; set; }
        public bool Success { get; set; }
    }

    public class ApiResponse
    {
        public string Message { get; set; }
        public string Error { get; set; }
        public HttpStatusCode ResponseCode { get; set; }
        public bool Success { get; set; }
    }
}
