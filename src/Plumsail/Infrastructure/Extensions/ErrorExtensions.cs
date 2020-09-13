using System.Net;
using Infrastructure.ErrorResult;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Extensions
{
    public static class ErrorExtensions
    {
        public static ActionResultException Throw(this ErrorModel error, HttpStatusCode statusCode)
        {
            return new ActionResultException(ToContentResult(error, statusCode));
        }

        public static ContentResult ToContentResult(this ErrorModel error, HttpStatusCode statusCode)
        {
            return new ContentResult()
            {
                StatusCode = (int)statusCode,
                ContentType = "application/json",
                Content = JObject.FromObject(error).ToString()
            };
        }
    }
}