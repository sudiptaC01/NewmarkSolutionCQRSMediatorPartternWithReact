using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Newmark_Technical_Assessment.ErrorHandler
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public  void OnException(ExceptionContext context)
        {
            try
            {
                GlobalExceptionHandler(context);
                LogException(context);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public  void LogException(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<CustomExceptionFilter>>();

            if (logger != null)
            {
                logger.LogError(context.Exception, "An error occurred");

                var result = new ObjectResult(new { Error = context.Exception.Message })
                {
                    StatusCode = context.Exception.Message.Contains("403") ?
                    StatusCodes.Status401Unauthorized :
                    context.Exception.Message.Contains("404") ?
                    StatusCodes.Status404NotFound :
                    StatusCodes.Status500InternalServerError
                };

                 context.Result = result;
            }
        }

        public  void GlobalExceptionHandler(ExceptionContext context)
        {
            var exception = context.Exception;
            var result =  new ObjectResult(new
            {
                Error = exception.Message,
                Details = exception.InnerException?.Message,
                StatusCode = context.Exception.Message.Contains("403") ?
                    StatusCodes.Status403Forbidden :
                    context.Exception.Message.Contains("404") ?
                    StatusCodes.Status404NotFound :
                    StatusCodes.Status500InternalServerError
            });

            context.Result =  result;
        }
    }
}
