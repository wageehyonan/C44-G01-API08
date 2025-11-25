using Azure;
using Ecommerce_G02.Domain.Exceptions;
using Ecommerce_G02.Shared.ErrorModels;
using System.Text.Json;

namespace Ecommerce_G02.Web.CustomMiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleWare> logger;

        public ExceptionMiddleWare(RequestDelegate Next,ILogger<ExceptionMiddleWare> Logger)
        {
            next = Next;
            this.logger = Logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                 await next.Invoke(context);

                if(context.Response.StatusCode==StatusCodes.Status404NotFound)
                {
                    var Response = new ErrorToReturn()

                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        ErrorMessage = $"End Point {context.Request.Path}Is Not Found"
                    };

                }
            }

            catch (Exception ex)
            {
                // logger.LogError(ex, "SomeThing Error");
                //or
                logger.LogError(ex,ex.Message);

                var Response = new ErrorToReturn()

                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message,
                };

                context.Response.StatusCode = ex switch

                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    UnAuthorizedException => StatusCodes.Status401Unauthorized,
                    BadRequestException badrequestexception=> GetBadRequestException(badrequestexception, Response),
                    _ => StatusCodes.Status500InternalServerError

                };
                    
                   
                context.Response.ContentType = "application/Jason";

              
                var reponseToRetrun=JsonSerializer.Serialize(Response);
                await context.Response.WriteAsync(reponseToRetrun);
            }
        }

        private static int GetBadRequestException(BadRequestException bad_request_exception,ErrorToReturn response)
        {
             response.Errors = bad_request_exception.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
