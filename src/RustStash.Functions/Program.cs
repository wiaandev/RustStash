using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RustStash.Core.Extensions;
using RustStash.Functions.Middlewares;

var host = new HostBuilder()
    // https://learn.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide#aspnet-core-integration
    .ConfigureFunctionsWebApplication(builder =>
    {
        builder.UseMiddleware<LoggingMiddleware>();
        builder.UseMiddleware<ExceptionHandlingMiddleware>();
    })
    .ConfigureAppConfiguration((context, config) =>
    {
        if (context.HostingEnvironment.IsProduction())
        {
            config.UseKeyVault();
        }
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();