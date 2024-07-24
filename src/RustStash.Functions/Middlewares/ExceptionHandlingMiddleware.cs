namespace RustStash.Functions.Middlewares;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using Sentry;

public class ExceptionHandlingMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var logger = context.GetLogger<ExceptionHandlingMiddleware>();
            if (IsCritical(context))
            {
#pragma warning disable CA2254
                logger.LogCritical(exception: ex, message: null);
#pragma warning restore CA2254
            }
            else
            {
#pragma warning disable CA2254
                logger.LogError(exception: ex, message: null);
#pragma warning restore CA2254
            }

            await SentrySdk.FlushAsync();

            throw;
        }
    }

    private static bool IsCritical(FunctionContext context)
    {
        // Used to make functions that are critically important
        return false;
    }
}