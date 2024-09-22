using System;
using LSL.AbstractConsole;
using Microsoft.Extensions.Logging;

namespace LSL.Tool.Logger;

/// <summary>
/// Logger used within the LSL.Tool.Template package
/// </summary>
public class DotNetToolLogger : ILogger
{
    private IConsole _console;

    /// <summary>
    /// Primary constructor
    /// </summary>
    /// <param name="console"></param>
    public DotNetToolLogger(IConsole console)
    {
        _console = console;
    }

    /// <inheritdoc/>
    public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;

    /// <inheritdoc/>
    public bool IsEnabled(LogLevel logLevel) => true;

    /// <inheritdoc/>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) =>
        _console
            .Write($"[{LogLevelToShortCode(logLevel)}] ")
            .WriteLine($"{formatter(state, exception)}");

    private static string LogLevelToShortCode(LogLevel logLevel) => logLevel switch
    {
        LogLevel.Debug => "DBG",
        LogLevel.Information => "INF",
        LogLevel.Warning => "WRN",
        LogLevel.Error => "ERR",
        LogLevel.Critical => "CRT",
        _ => "TRC"
    };
}