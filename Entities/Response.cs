
using System.Net;

namespace Entities
{
    public class Response<T> : IResponse<T>
    {
        public HttpStatusCode HttpCode { get; set; }
        public bool HasError { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }

        public void SetCreated(T result, string message = "Created", bool hasError = false)
        {
            HttpCode = HttpStatusCode.Created;
            HasError = hasError;
            Message = message;
            Result = result;
        }
        public Response<T> GetResponse<J>(Response<J> response)
        {
            return new Response<T>
            {
                HttpCode = response.HttpCode,
                HasError = response.HasError,
                Message = response.Message,
                Result = default(T)
            };
        }
        public Response<T> GetSuccess(T result, string message = "OK")
        {
            return new Response<T>
            {
                HttpCode = HttpStatusCode.OK,
                HasError = false,
                Message = message,
                Result = result
            };
        }
        public Response<T> GetBadRequest(string message)
        {
            return new Response<T>
            {
                HttpCode = HttpStatusCode.BadRequest,
                HasError = true,
                Message = message,
                Result = default(T)
            };
        }

        public Response<T> GetNotFound(string message)
        {
            return new Response<T>
            {
                HttpCode = HttpStatusCode.NotFound,
                HasError = true,
                Message = message,
                Result = default(T)
            };
        }

        public Response<T> GetError(string message)
        {
            return new Response<T>
            {
                HttpCode = HttpStatusCode.InternalServerError,
                HasError = true,
                Message = message,
                Result = default(T)
            };
        }

        public Response<T> GetUnauthorized(string message)
        {
            return new Response<T>
            {
                HttpCode = HttpStatusCode.Unauthorized,
                HasError = true,
                Message = message,
                Result = default(T)
            };
        }

        public void SetSuccess(T result, string message = "OK")
        {
            HttpCode = HttpStatusCode.OK;
            HasError = false;
            Message = message;
            Result = result;
        }

        public void SetError(Exception exception)
        {
            HttpCode = HttpStatusCode.InternalServerError;
            HasError = true;
            Message = exception.Message;
            Result = default;
        }

        public void SetError(string message)
        {
            HttpCode = HttpStatusCode.InternalServerError;
            HasError = true;
            Message = message;
            Result = default;
        }
    }
}
