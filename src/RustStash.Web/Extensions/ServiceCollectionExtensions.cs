// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

using HotChocolate.Types;
using HotChocolate.Types.NodaTime;
using HotChocolate.Types.Pagination;
using RustStash.Core;
using RustStash.Web;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGraph(this IServiceCollection services)
    {
        services.AddGraphQLServer()
            // until https://github.com/ChilliCream/hotchocolate/issues/4790 is fixed
            .SetPagingOptions(new PagingOptions
            {
                InferConnectionNameFromField = false,
            })
            .AddWebTypes()
            .AddMutationConventions()
            .RegisterDbContext<AppDbContext>(DbContextKind.Pooled)
            .AddErrorFilter<ErrorFilter>()
            .AddDiagnosticEventListener<MyExecutionDiagnosticEventListener>()
            .AddDiagnosticEventListener<MyDataLoaderEventListener>()
            .AddGlobalObjectIdentification()
            .AddAuthorization()
            .AddType(new TimeSpanType(TimeSpanFormat.DotNet))
            .AddType<OffsetDateTimeType>()
            .AddType<LocalDateType>()
            .AddType<LocalTimeType>();

        return services;
    }
}
