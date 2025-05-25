using System;

namespace LSL.Tool.Logger;

internal static class GuardExtensions
{
    public static T GuardAgainstNull<T>(this T source, string parameterName)
    {
        if (source == null) throw new ArgumentNullException(parameterName);

        return source;
    }
}
