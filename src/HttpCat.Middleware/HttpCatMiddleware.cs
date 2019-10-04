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
                    await context.Response.WriteAsync(@"<!DOCTYPE html><html><head><style>body { background-color: black; } img { position: absolute; top: 50%; left: 50%; width: 750px; height: 600px; margin-left: -375px; /* Half the width */ margin-top: -300px; /* Half the height */ }</style></head><body><img src=""" + $"https://http.cat/{context.Response.StatusCode}" + $"\" alt=\"{context.Response.StatusCode}\" /></body></html>");
                }
            }
        }
    }
}
