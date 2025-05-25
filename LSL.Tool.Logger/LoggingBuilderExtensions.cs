using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LSL.Tool.Logger;

/// <summary>
/// LoggingBuilderExtensions
/// </summary>
public static class LoggingBuilderExtensions
{
    /// <summary>
    /// Add the DotNetToolLogger to the logging infrastructure
    /// </summary>
    /// <param name="source"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static ILoggingBuilder AddDotNetToolLogger(this ILoggingBuilder source, Action<DotNetToolLoggerOptions> configurator = null)
    {
        source
            .Services
            .AddSingleton<ILoggerProvider, DotNetToolLoggerProvider>()
            .Configure(configurator ?? SetupDefaultOptions);

        return source;
    }

    internal static void SetupDefaultOptions(DotNetToolLoggerOptions options) { }            
}