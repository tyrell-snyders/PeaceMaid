using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PeaceMaid.Presentation.WebAPI.Middleware
{
    public class TraceIdLoggerMiddleware(RequestDelegate next, ILogger<TraceIdLoggerMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<TraceIdLoggerMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext ctx)
        {
            var traceId = ctx.TraceIdentifier;
            _logger.LogInformation("Handling request with TraceId: {TraceId}", traceId);

            await _next(ctx);
        }
    }
}
