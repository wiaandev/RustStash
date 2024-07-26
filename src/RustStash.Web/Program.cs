using Newtonsoft.Json;
using RustStash.Core;
using RustStash.Core.Entities.Auth;
using RustStash.Core.Extensions;
using RustStash.Web;
using RustStash.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    builder.Configuration.UseKeyVault();
}
else
{
    builder.Configuration.AddJsonFileAfterLastJsonFile(
        "appsettings.Local.json",
        optional: true,
        reloadOnChange: false);
}

builder.Services.AddDb(builder.Configuration);
builder.Services.AddCoreServices();
builder.Services.AddAuth();
builder.Services.AddSingleton<ISessionContext, HttpSessionContext>();
builder.Services.AddGraph();
builder.Services.AddHealthChecks();

var app = builder.Build();

await app.Initialize();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapGroup("/api/account")
    .MapAccountEndpoints();

app.MapGraphQL();
app.MapHealthChecks("/healthz");
app.MapFallbackToFile("index.html");

// Add middleware to log schema initialization errors
app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;
        var response = new { error = ex.Message };
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
});

app.Run();

public partial class Program
{
}