using System;
using System.Diagnostics.CodeAnalysis;
using LSL.AbstractConsole;
using Microsoft.Extensions.Logging;

namespace LSL.Tool.Logger;

/// <summary>
/// Logger Provider for the <c cref="DotNetToolLogger">DotNetToolLogger</c>
/// </summary>
public class DotNetToolLoggerProvider : ILoggerProvider
{
    private readonly IConsole _console;

    /// <summary>
    /// Primary constructor
    /// </summary>
    /// <param name="console"></param>
    public DotNetToolLoggerProvider(IConsole console)
    {
        _console = console;
    }

    /// <inheritdoc/>
    public ILogger CreateLogger(string categoryName) => new DotNetToolLogger(_console);

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public void Dispose() => GC.SuppressFinalize(this);
}