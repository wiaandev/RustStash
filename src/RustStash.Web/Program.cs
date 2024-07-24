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
builder.WebHost.AddSentry();

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
    .MapIdentityApi<User>();

app.MapGroup("/api/account")
    .MapAccountEndpoints();

app.MapGraphQL();
app.MapFallbackToFile("index.html");

app.Run();

public partial class Program
{
}