﻿using Microsoft.AspNetCore.Http;
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
                    await context.Response.WriteAsync(@"<!DOCTYPE html><html><head><style>body { background-color: black; } img { display: block; margin:auto; }</style></head><body><img src=""" + $"https://http.cat/{context.Response.StatusCode}" + $"\" alt=\"{context.Response.StatusCode}\" /></body></html>");
                }
            }
        }
    }
}
