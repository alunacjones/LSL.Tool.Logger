using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using LSL.AbstractConsole.ServiceProvider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static LSL.Tool.Logger.Tests.Tests;

namespace LSL.Tool.Logger.Tests;

public class CustomOptionsTests
{
    [Test]
    public void GivenCallsToTheLoggerWithTheTimeStampBuilder_ThenItShouldProduceTheExpectedLogs()
    {
        // Arrange
        using var writer = new StringWriter();

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services
                    .AddAbstractConsole(s => s.TextWriter = writer)
                    .AddSingleton<LoggingTesting>()
                    .AddLogging(l =>
                    {
                        l.ClearProviders();

                        l.AddDotNetToolLogger(
                            c => c.UseTimeStampLoggingOutputBuilder()
                        );
                        l.SetMinimumLevel(LogLevel.Trace);
                    });

            })
            .Build();

        // Act
        var test = host.Services.GetRequiredService<LoggingTesting>();
        test.LogAllLevels();

        // Assert
        var lines = writer.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var expectedMessages = new[]
        {
            "CRT als",
            "DBG als",
            "ERR als",
            "INF als",
            "TRC als",
            "WRN als"
        };

        var index = 0;

        foreach (var line in lines.Skip(1))
        {
            //[2025-05-25 15:05:26.6503] CRT als
            line.Should().MatchRegex(@$"^\[\d{{4}}-\d{{2}}-\d{{2}} \d{{2}}:\d{{2}}:\d{{2}}.\d{{4}}] {line[27..]}");
            index++;
        }
        ;
    }

    [Test]
    public void GivenCallsToTheLoggerWithTheCustomTimeStampBuilder_ThenItShouldProduceTheExpectedLogs()
    {
        // Arrange
        using var writer = new StringWriter();

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services
                    .AddAbstractConsole(s => s.TextWriter = writer)
                    .AddSingleton<LoggingTesting>()
                    .AddLogging(logging =>
                    {
                        logging.
                            ClearProviders()
                            .AddDotNetToolLogger(
                                c => c.UseTimeStampLoggingOutputBuilder("hh:mm:ss")
                            ).SetMinimumLevel(LogLevel.Trace);
                    });

            })
            .Build();

        // Act
        var test = host.Services.GetRequiredService<LoggingTesting>();
        test.LogAllLevels();

        // Assert
        var lines = writer.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var expectedMessages = new[]
        {
            "CRT als",
            "DBG als",
            "ERR als",
            "INF als",
            "TRC als",
            "WRN als"
        };

        var index = 0;

        foreach (var line in lines.Skip(1))
        {
            line.Should().MatchRegex(@"^\[\d{2}:\d{2}:\d{2}] " + line[11..]);
            index++;
        }
        ;
    }

    [Test]
    public void GivenCallsToTheLoggerWithACustomBuilderBuilder_ThenItShouldProduceTheExpectedLogs()
    {
        // Arrange
        using var writer = new StringWriter();

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services
                    .AddAbstractConsole(s => s.TextWriter = writer)
                    .AddSingleton<LoggingTesting>()
                    .AddLogging(l =>
                    {
                        l.ClearProviders();

                        l.Services.AddDotNetToolLogger(
                            c => c.LoggingOutputBuilder = context => $"{context.LogLevel} {context.FormattedMessage}"
                        );
                        l.SetMinimumLevel(LogLevel.Trace);
                    });

            })
            .Build();

        // Act
        var test = host.Services.GetRequiredService<LoggingTesting>();
        test.LogAllLevels();

        // Assert
        writer.ToString().Should().Be(
            """
            Information Trace enabled: True
            Critical als
            Debug als
            Error als
            Information als
            Trace als
            Warning als

            """.ReplaceLineEndings()
        );
    }
}
