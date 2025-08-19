using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Web
{
    /// <summary>
    /// Фильтр обработки ошибок
    /// </summary>
    public class SchoolApplicationExceptionFilter : IExceptionFilter
    {
        void IExceptionFilter.OnException(ExceptionContext context)
        {
            if (context.Exception is not SchoolApplicationException exception)
            {
                return;
            }

            switch (exception)
            {
                case SchoolApplicationNotFoundException ex:
                    SetDataToContext(new NotFoundObjectResult(new ApiExceptionDetail(ex.Message)), context);
                    break;
                case SchoolApplicationInvalidOperationException ex:
                    SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail(ex.Message))
                    {
                        StatusCode = StatusCodes.Status406NotAcceptable,
                    }, context);
                    break;
                case SchoolApplicationValidationException ex:
                    SetDataToContext(new BadRequestObjectResult(new ApiValidationExceptionDetail()
                    {
                        Errors = ex.Errors,
                    })
                    {
                        StatusCode = StatusCodes.Status422UnprocessableEntity,
                    }, context);
                    break;
                default:
                    SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail(exception.Message)), context);
                    break;
            }
        }

        private static void SetDataToContext(ObjectResult data, ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var response = context.HttpContext.Response;
            response.StatusCode = data.StatusCode ?? StatusCodes.Status400BadRequest;
            context.Result = data;
        }
    }
}
