namespace EmployeeAPI.MiddleWare
{
    public static class LogMiddlewareExtensionMethods
    {
        public static IApplicationBuilder UseLogMiddleware (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}
