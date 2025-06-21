using SimpleLogger.Services;

namespace SimpleLogger.Middlewares
{
    public class UseRequestLogging
    {
        RequestDelegate next;
        ILogger log;
        public UseRequestLogging(RequestDelegate next,ILogger log)
        {
            this.log = log;
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var now = DateTime.Now;
            try
            {
                await next(context);
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
            }
            
            string message = $"[{now}] {context.Request.Method} {context.Request.Path} -> {context.Response.StatusCode}";

            var statusString = context.Response.StatusCode switch
            {
                >= 200 and < 300 => "OK",
                >= 400 and < 500 => "Client Error",
                >= 500 => "Server Error",
                _ => "Unknown"
            };
            
            message += $" {statusString} {Math.Round((DateTime.Now-now).TotalMilliseconds)} ms";
            log.LogInformation(message);

            

        }


    }
}
