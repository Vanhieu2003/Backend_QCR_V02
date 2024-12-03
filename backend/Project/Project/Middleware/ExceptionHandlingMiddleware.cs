using Newtonsoft.Json;
using System.Net;

namespace Project.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public RequestDelegate requestDelegate;
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.requestDelegate = requestDelegate;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private Task HandleException(HttpContext context, Exception ex)
        {
            _logger.LogError(new EventId(1, "UnhandledException"), ex, ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorMessage = JsonConvert.SerializeObject(new { Message = ex.Message });

            return context.Response.WriteAsync(errorMessage);
        }
    }
}
