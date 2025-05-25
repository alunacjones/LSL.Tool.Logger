using Microsoft.Extensions.Logging;

namespace LSL.Tool.Logger;

/// <summary>
/// The context passed into a <see cref="DotNetToolLoggerOptions.LoggingOutputBuilder"/> instance
/// </summary>
public class OutputContext
{
    internal OutputContext(string formattedMessage, string logLevelShortCode, LogLevel logLevel)
    {
        FormattedMessage = formattedMessage;
        LogLevelShortCode = logLevelShortCode;
        LogLevel = logLevel;
    }
    
    /// <summary>
    /// The formatted log message
    /// </summary>
    public string FormattedMessage { get; }

    /// <summary>
    /// The log level as a short code
    /// </summary>
    public string LogLevelShortCode { get; }

    /// <summary>
    /// The actual <see cref="LogLevel" /> in case you want to use custom output for it
    /// </summary>
    public LogLevel LogLevel { get; }
}