using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Data.cs.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCaching;

namespace Data.cs
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
        public void SetSuccess (T result, string message = "OK")
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
        public void SetError(string massage)
        {
            HttpCode = HttpStatusCode.InternalServerError;
            HasError = true;
            Message = massage;
            Result = default;
        }
    }
}
