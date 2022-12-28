using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using k8s.Autorest;
using System.Net;
using FleetManager.Exceptions;

namespace FleetManager.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        private readonly Dictionary<Type, Func<Exception, HttpStatusCode>> ExceptionResponseCodeMap = new()
        {
            { typeof(HttpOperationException), HandleHttpOperationException },
            { typeof(GameServerAllocationException), _ => HttpStatusCode.BadRequest }
        };

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is null)
            {
                return;
            }

            var responseCodeFunc = ExceptionResponseCodeMap.GetValueOrDefault(context.Exception.GetType(),
                _ => HttpStatusCode.InternalServerError);
            context.Result = new ObjectResult(new GenericHttpResponse(context.Exception.Message ?? "An unexpected error has occurred"))
                {
                    StatusCode = (int)responseCodeFunc(context.Exception)
                };
            context.ExceptionHandled = true;
        }

        private static HttpStatusCode HandleHttpOperationException(Exception exception)
        {
            var castedException = (HttpOperationException)exception;
            if (castedException.Response.StatusCode == HttpStatusCode.NotFound)
            {
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.InternalServerError;
        }
    }

    record GenericHttpResponse(params string[] Errors);
}
