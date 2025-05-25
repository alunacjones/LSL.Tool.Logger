using System;
using System.IO;
using System.Text;
using FluentAssertions;
using LSL.AbstractConsole;
using Microsoft.Extensions.Logging;

namespace LSL.Tool.Logger.Tests;

public class ManualSetupTests
{
    [Test]
    public void GivenALegacyConstructorCall_ItShouldProduceTheExpectedOutput()
    {
        var writer = new StringWriter(new StringBuilder());
        var loggingFactory = new LoggerFactory();
        loggingFactory.AddProvider(new DotNetToolLoggerProvider(new DefaultConsole(writer)));
        var logger = loggingFactory.CreateLogger("test");
        logger.LogInformation("test output");

        writer.ToString().Should().Be($"[INF] test output{Environment.NewLine}");
    }
}
