namespace SerilogLogging.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestContextLogging(
        this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestContextLoggingMiddleware>();

            return app;
        }
    }
}
