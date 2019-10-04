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

        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);
        }
    }
}
