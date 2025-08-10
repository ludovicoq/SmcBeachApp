using Mch.MainManagerSrv.Data.Models;
using Mch.MainManagerSrv.Resources.Localization;
using Microsoft.Extensions.Localization;

namespace SmBeachApp.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<TranslationStrings> _localizer;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, IStringLocalizer<TranslationStrings> localizer, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _localizer = localizer;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                // Do some stuff...
                // Log exception

                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResponse = new ErrorResponse();

            if (exception is HttpException httpException)
            {
                errorResponse.StatusCode = httpException.StatusCode;
                errorResponse.Message = _localizer.GetString(httpException?.Message, httpException.Arguments);
                _logger.LogDebug($"Handled Exception: \n Message: {errorResponse.Message}");
            }
            else
            {
                errorResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                errorResponse.Message = $"Internal Server Error \n {exception.Message}";
                _logger.LogError(exception, "Unhandled Exception");
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)errorResponse.StatusCode;
            await context.Response.WriteAsync(errorResponse.ToJsonString());
        }
    }
}
