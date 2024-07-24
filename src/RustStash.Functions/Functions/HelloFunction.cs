namespace RustStash.Functions.Functions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

public class HelloFunction
{
    [Function(nameof(HelloFunction))]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {
        return new OkObjectResult($"Welcome to Azure Functions, {req.Query["name"]}!");
    }
}