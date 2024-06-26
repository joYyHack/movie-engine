using System.Net;

namespace Movies.BL.Models
{
    /// <summary>
    /// Represents a standardized response structure for API operations.
    /// </summary>
    /// <typeparam name="T">The type of the data being returned.</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Gets or sets the HTTP status code of the response.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation failed.
        /// </summary>
        public bool Failed { get; set; }

        /// <summary>
        /// Gets a value indicating whether the operation succeeded.
        /// </summary>
        public bool Succeeded => !Failed;

        /// <summary>
        /// Gets or sets the error message associated with the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data being returned in the response.
        /// </summary> 
        public T Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response{T}"/> class.
        /// </summary>
        public Response()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response{T}"/> class with the specified data and message.
        /// </summary>
        /// <param name="data">The data being returned in the response.</param>
        /// <param name="message">The message associated with the response.</param>
        public Response(T data, string message = null)
        {
            Message = message;
            Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response{T}"/> class with the specified error message.
        /// </summary>
        /// <param name="message">The error message associated with the response.</param>
        public Response(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Implicitly converts the specified data to a <see cref="Response{T}"/> instance.
        /// </summary>
        /// <param name="data">The data being returned in the response.</param>
        public static implicit operator Response<T>(T data) => new(data) { HttpStatusCode = HttpStatusCode.OK };

        /// <summary>
        /// Implicitly converts the specified tuple to a <see cref="Response{T}"/> instance.
        /// </summary>
        /// <param name="tuple">The tuple containing the message and HTTP status code.</param>
        public static implicit operator Response<T>((string message, HttpStatusCode httpStatusCode) tuple) =>
            new(tuple.message) { HttpStatusCode = tuple.httpStatusCode, Failed = true };
    }
}
