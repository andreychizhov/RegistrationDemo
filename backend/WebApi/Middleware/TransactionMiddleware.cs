using Microsoft.AspNetCore.Http;
using PM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    public class TransactionMiddleware
    {
        private readonly RequestDelegate _next;

        public TransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IDbSession dbSession)
        {
            await _next(httpContext);

            await dbSession.CommitChangesAsync();
        }
    }
}
