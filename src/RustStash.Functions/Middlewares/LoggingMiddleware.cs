namespace RustStash.Functions.Middlewares;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

public class LoggingMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var name = context.FunctionDefinition.Name;
        var logger = context.GetLogger(name);
        logger.LogInformation("function executed at: {}", DateTime.Now);
        try
        {
            await next(context);
        }
        finally
        {
            logger.LogInformation("function completed: {}", DateTime.Now);
        }
    }
}