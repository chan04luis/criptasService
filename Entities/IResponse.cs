using System.Net;

namespace Entities
{
    public interface IResponse<T>
    {
        bool HasError { get; set; }
        HttpStatusCode HttpCode { get; set; }
        string Message { get; set; }
        T Result { get; set; }
        void SetError(Exception exception);
        void SetError(string message);
        Response<T> GetBadRequest(string message);
        Response<T> GetNotFound(string message);
        Response<T> GetResponse<J>(Response<J> response);
        Response<T> GetError(string message);
        Response<T> GetSuccess(T result, string message = "OK");
        Response<T> GetUnauthorized(string message);
        void SetSuccess(T result, string message = "OK");
        void SetCreated(T result, string message = "Created", bool hasError = false);
    }
}
