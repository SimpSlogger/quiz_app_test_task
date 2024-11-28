using System.ComponentModel.DataAnnotations;
using System.Net;
using TestsManager.Common;

namespace TestsManager.WebApi.ExceptionHandlers
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ILogger logger;

        public ExceptionHandlingMiddleware(RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            this.next = next;
            logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            logger.LogError(exception, exception.Message);

            var codeInfo = Map(exception);

            //var result = JsonConvert.SerializeObject(new HttpExceptionWrapper((int)codeInfo.Code, codeInfo.Message));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)codeInfo.Code;
            return context.Response.WriteAsync(codeInfo.Message);
        }
        
        public static HttpStatusCodeInfo Map(Exception exception)
        {
            var code = exception switch
            {
                UnauthorizedAccessException _ => HttpStatusCode.Unauthorized,
                NotImplementedException _ => HttpStatusCode.NotImplemented,
                InvalidOperationException _ => HttpStatusCode.Conflict,
                ArgumentException _ => HttpStatusCode.BadRequest,
                ValidationException _ => HttpStatusCode.BadRequest,
                TableParsException _ => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            return new HttpStatusCodeInfo(code, exception.Message);
        }
    }
}
