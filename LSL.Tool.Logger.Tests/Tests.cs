using System.IO;
using FluentAssertions;
using LSL.AbstractConsole.ServiceProvider;
using LSL.AppVeyor.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LSL.Tool.Logger.Tests;

public class Tests
{
    [Test]
    public void GivenCallsToTheLogger_ThenItShouldProduceTheExpectedLogs()
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

                        l.Services.AddDotNetToolLogger();
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
            [INF] Trace enabled: True
            [CRT] als
            [DBG] als
            [ERR] als
            [INF] als
            [TRC] als
            [WRN] als

            """.FixStringConstantNewLines()
        );
    }

    public class LoggingTesting(ILogger<LoggingTesting> logger)
    {
        public void LogAllLevels()
        {
            const string message = "als";

            using var _ = logger.BeginScope("state");

            logger.LogInformation("Trace enabled: {isEnabled}", logger.IsEnabled(LogLevel.Trace));
            logger.LogCritical(message);
            logger.LogDebug(message);
            logger.LogError(message);
            logger.LogInformation(message);
            logger.LogTrace(message);
            logger.LogWarning(message);
        }
    }
}