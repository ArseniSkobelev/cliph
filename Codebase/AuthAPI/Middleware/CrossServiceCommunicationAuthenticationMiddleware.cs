using cliph.Library;

namespace cliph.Middleware;

public class CrossServiceCommunicationAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public CrossServiceCommunicationAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("x-cliph-cross-service-authentication", out var headerValue))
        {
            if (string.IsNullOrWhiteSpace(headerValue))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized; 
                await context.Response.WriteAsync("");
            }
            
            if (IsValidKey(headerValue))
            {
                await _next(context);
                return;
            }
        }

        // Custom header is missing or invalid, return an error response
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("");
    }

    private bool IsValidKey(string headerValue)
    {
        string privateKey =
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration,
                "CrossServiceCommunicationAuthentication:Secret");
        return headerValue == privateKey;
    }
}