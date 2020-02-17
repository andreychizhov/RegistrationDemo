using Microsoft.AspNetCore.Builder;
using WebApi.Middleware;

namespace WebApi.Helpers
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseTransactionScopeMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TransactionMiddleware>();
        }
    }
}
