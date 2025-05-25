using System;
using FluentAssertions;
using LSL.AbstractConsole;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace LSL.Tool.Logger.Tests;

public class NullGuardTestsAgainstPublicApi
{
    [TestCase(false, true, "options")]
    [TestCase(true, false, "console")]
    [TestCase(true, true, "console")]
    public void WhenCreatingAProviderWithAnyNullValue_ItShouldThrowAnArgumentNullException(bool nullConsole, bool nullOptions, string expectedParamName)
    {
        var console = nullConsole ? null : Substitute.For<IConsole>();
        var options = nullOptions ? null : Substitute.For<IOptionsMonitor<DotNetToolLoggerOptions>>();
        options?.CurrentValue.Returns(new DotNetToolLoggerOptions());

        new Action(() => _ = new DotNetToolLoggerProvider(console, options))
            .Should()
            .Throw<ArgumentNullException>()
            .And
            .ParamName
            .Should()
            .Be(expectedParamName);
    }
}