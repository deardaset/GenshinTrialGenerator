using GenshinTrialGenerator.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace GenshinTrialGenerator.Server.Exceptions
{
    public class GeneratorExceptionHandler(ILogger<GeneratorExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Exception occured: {Message}", exception.Message);

            var statuscode = exception switch
            {
                GeneratorException x => x.StatusCode,
                _ => StatusCodes.Status500InternalServerError
            };
            httpContext.Response.StatusCode = statuscode;

            var error = new
            {
                id = Guid.NewGuid(),
                StatusCode = statuscode,
                ErrorMessage = exception.Message
            };

            await httpContext.Response.WriteAsJsonAsync(error, cancellationToken);

            return true;
        }
    }
}
