using System.Net;

namespace Movies.BL.Models
{
    public class Response<T>
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public bool Failed { get; set; }
        public bool Succeeded => !Failed;
        public string Message { get; set; }
        public T Data { get; set; }

        public Response()
        {
        }
        public Response(T data, string message = null)
        {
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            Message = message;
        }

        public static implicit operator Response<T>(T data) => new(data) { HttpStatusCode = HttpStatusCode.OK };

        public static implicit operator Response<T>((string message, HttpStatusCode httpStatusCode) tuple) =>
            new(tuple.message) { HttpStatusCode = tuple.httpStatusCode, Failed = true };
    }
}
