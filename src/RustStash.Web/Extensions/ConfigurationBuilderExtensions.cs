namespace RustStash.Web.Extensions;

using Microsoft.Extensions.Configuration.Json;

public static class ConfigurationBuilderExtensions
{
    // Inject appsettings.Local.json after the environment specific settings
    public static IConfigurationBuilder AddJsonFileAfterLastJsonFile(
        this IConfigurationBuilder builder,
        string path,
        bool optional,
        bool reloadOnChange)
    {
        var jsonFileSource = new JsonConfigurationSource
        {
            FileProvider = null,
            Path = path,
            Optional = optional,
            ReloadOnChange = reloadOnChange,
        };
        jsonFileSource.ResolveFileProvider();

        // appsettings.[environment].json
        var lastJsonFileSource = builder.Sources.LastOrDefault(s => s is FileConfigurationSource);

        if (lastJsonFileSource is not null)
        {
            var indexOfLastJsonFileSource = builder.Sources.IndexOf(lastJsonFileSource);
            builder.Sources.Insert(
                indexOfLastJsonFileSource == -1
                    ? builder.Sources.Count
                    : indexOfLastJsonFileSource + 1,
                jsonFileSource);
        }
        else
        {
            builder.Sources.Add(jsonFileSource);
        }

        return builder;
    }
}