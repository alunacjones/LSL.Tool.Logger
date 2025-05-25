using System;
using LSL.AbstractConsole;
using Microsoft.Extensions.Logging;

namespace LSL.Tool.Logger;

/// <summary>
/// Logger used within the LSL.Tool.Template package
/// </summary>
public class DotNetToolLogger : ILogger
{
    private readonly IConsole _console;
    private readonly DotNetToolLoggerOptions _options;

    internal DotNetToolLogger(IConsole console, DotNetToolLoggerOptions options)
    {
        _console = console;
        _options = options;
    }

    /// <inheritdoc/>
    public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;

    /// <inheritdoc/>
    public bool IsEnabled(LogLevel logLevel) => true;

    /// <inheritdoc/>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        var context = new OutputContext(formatter(state, exception), LogLevelToShortCode(logLevel), logLevel);
        _console.WriteLine(_options.LoggingOutputBuilder(context));
    }

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