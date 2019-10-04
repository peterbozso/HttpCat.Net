using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HttpCat.Middleware
{
    public class HttpCatMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpCatMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch
            {
                if (context.Response.HasStarted)
                {
                    throw;
                }

                context.Response.StatusCode = 500;
            }
            finally
            {
                if (!context.Response.HasStarted)
                {
                    await context.Response.WriteAsync($"https://http.cat/{context.Response.StatusCode}");
                }
            }
        }
    }
}
