using System;

namespace LSL.Tool.Logger;

/// <summary>
/// Options for the <see cref="DotNetToolLogger"/>
/// </summary>
public class DotNetToolLoggerOptions
{
    /// <summary>
    /// A delegate to create the logger output
    /// </summary>
    /// <remarks>
    /// The default format is <c>[{ShortLoggingLevel}] {FormattedMessage}</c>
    /// </remarks>
    public Func<OutputContext, string> LoggingOutputBuilder { get; set; }

    /// <summary>
    /// And output builder that outputs logs in the format
    /// <c>[{TimeStamp}] {ShortLoggingLevel} {FormattedMessage}</c>
    /// </summary>
    /// <returns></returns>
    public DotNetToolLoggerOptions UseTimeStampLoggingOutputBuilder(string timeStampFormat = null)
    {
        LoggingOutputBuilder = TimeStampLoggingOutputBuilderFactory(timeStampFormat);
        return this;
    }

    internal DotNetToolLoggerOptions Validate()
    {
        LoggingOutputBuilder ??= DefaultLoggingOutputBuilder;
        return this;
    }

    internal Func<OutputContext, string> TimeStampLoggingOutputBuilderFactory(string timeStampFormat)
    {
        return context => $"[{FormatDate()}] {context.LogLevelShortCode} {context.FormattedMessage}";

        string FormatDate() => DateTime.Now.ToString(timeStampFormat ?? "yyyy-MM-dd HH:mm:ss.ffff");
    }

    internal string DefaultLoggingOutputBuilder(OutputContext context) =>
        $"[{context.LogLevelShortCode}] {context.FormattedMessage}";
}
