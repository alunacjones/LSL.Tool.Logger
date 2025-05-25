using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LSL.Tool.Logger;

/// <summary>
/// LoggingServiceCollectionExtensions
/// </summary>
public static class LoggingServiceCollectionExtensions
{
    /// <summary>
    /// Add the DotNetToolLogger to a service collection
    /// </summary>
    /// <param name="source"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    [Obsolete("Use the ILoggingBuilder version instead")]
    public static IServiceCollection AddDotNetToolLogger(this IServiceCollection source, Action<DotNetToolLoggerOptions> configurator = null) =>
        source
            .AddSingleton<ILoggerProvider, DotNetToolLoggerProvider>()
            .Configure(configurator ?? LoggingBuilderExtensions.SetupDefaultOptions);    
}
