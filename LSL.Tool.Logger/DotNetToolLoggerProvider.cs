using System;
using System.Diagnostics.CodeAnalysis;
using LSL.AbstractConsole;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LSL.Tool.Logger;

/// <summary>
/// Logger Provider for the <c cref="DotNetToolLogger">DotNetToolLogger</c>
/// </summary>
public class DotNetToolLoggerProvider : ILoggerProvider
{
    private readonly IConsole _console;
    private readonly DotNetToolLoggerOptions _options;

    /// <summary>
    /// Secondary constructor (legacy)
    /// </summary>
    /// <param name="console"></param>
    public DotNetToolLoggerProvider(IConsole console)
    {
        _console = console;
        _options = new DotNetToolLoggerOptions().Validate();
    }

    /// <summary>
    /// Primary constructor
    /// </summary>
    /// <param name="console"></param>
    /// <param name="options"></param>
    public DotNetToolLoggerProvider(IConsole console, IOptionsMonitor<DotNetToolLoggerOptions> options)
    {
        _console = console.GuardAgainstNull(nameof(console));
        _options = options.GuardAgainstNull(nameof(options))
            .CurrentValue
            .Validate();
    }

    /// <inheritdoc/>
    public ILogger CreateLogger(string categoryName) => new DotNetToolLogger(_console, _options);

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public void Dispose() => GC.SuppressFinalize(this);
}