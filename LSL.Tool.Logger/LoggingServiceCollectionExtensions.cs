using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LSL.Tool.Logger;

/// <summary>
/// LoggingBuilderExtensions
/// </summary>
public static class LoggingServiceCollectionExtensions
{
    /// <summary>
    /// Add the DotNetToolLogger to a service collection
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IServiceCollection AddDotNetToolLogger(this IServiceCollection source) => 
        source.AddSingleton<ILoggerProvider, DotNetToolLoggerProvider>();
}