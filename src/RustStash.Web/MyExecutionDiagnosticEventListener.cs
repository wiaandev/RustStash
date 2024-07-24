namespace RustStash.Web;

using System;
using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using Microsoft.Extensions.Logging;

public class MyExecutionDiagnosticEventListener : ExecutionDiagnosticEventListener
{
    private readonly ILogger<MyExecutionDiagnosticEventListener> logger;

    public MyExecutionDiagnosticEventListener(ILogger<MyExecutionDiagnosticEventListener> logger)
    {
        this.logger = logger;
    }

    public override void RequestError(
        IRequestContext context,
        Exception exception)
    {
        this.logger.LogError(
            exception,
            "GraphQL Query Failed, Message: {}, Operation: {}",
            exception.Message,
            context.Operation?.Name);
    }
}
