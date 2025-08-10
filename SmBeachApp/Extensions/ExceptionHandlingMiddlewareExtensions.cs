using Microsoft.AspNetCore.Builder;
using SmBeachApp.Middlewares;

namespace Mch.MainManagerSrv.Extensions
{
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            return app;
        }
    }
}
