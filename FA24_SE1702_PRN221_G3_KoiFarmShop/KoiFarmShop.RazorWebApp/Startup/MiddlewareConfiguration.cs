using KoiFarmShop.Infrastructure.Middleware;

namespace KoiFarmShop.RazorWebApp.Startup
{
    public static class MiddlewareConfiguration
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
