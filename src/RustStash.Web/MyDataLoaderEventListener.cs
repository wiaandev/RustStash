namespace RustStash.Web;

using System;
using System.Collections.Generic;
using GreenDonut;
using Microsoft.Extensions.Logging;

public class MyDataLoaderEventListener : DataLoaderDiagnosticEventListener
{
    private readonly ILogger<MyDataLoaderEventListener> logger;

    public MyDataLoaderEventListener(ILogger<MyDataLoaderEventListener> logger)
    {
        this.logger = logger;
    }

    public override void BatchError<TKey>(IReadOnlyList<TKey> keys, Exception error)
    {
        this.logger.LogError(
            error,
            "BatchError, Keys: {}",
            keys);
    }

    public override void BatchItemError<TKey>(TKey key, Exception error)
    {
        this.logger.LogError(
            error,
            "BatchItemError, Key: {}",
            key);
    }
}
