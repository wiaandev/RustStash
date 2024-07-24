namespace RustStash.Core.Extensions;

using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

public static class IConfigurationBuilderExtensions
{
    public static void UseKeyVault(this IConfigurationBuilder config)
    {
        var builtConfig = config.Build();
        var keyVaultName = builtConfig["KeyVaultName"];

        if (!string.IsNullOrWhiteSpace(keyVaultName))
        {
            var secretClient = new SecretClient(
                new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                new DefaultAzureCredential());
            config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
        }
    }
}