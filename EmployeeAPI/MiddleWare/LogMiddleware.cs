namespace EmployeeAPI.MiddleWare
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            Console.WriteLine("Hi");
            Console.WriteLine("This is a log Middleware");

            await _next(context);
            Console.WriteLine("This is the end of log Middleware");
        }
    }
}
