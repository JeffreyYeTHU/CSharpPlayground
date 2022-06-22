using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ThrowHelper;

internal static class ThrowUtil
{
    public static void ThrowIfNullOrEmpty(
        string s,
        [CallerMemberName] string? caller = null,
        [CallerArgumentExpression("s")] string? callerExpr = null,
        [CallerFilePath] string? filePath = null,
        [CallerLineNumber] int lineNum = 0)
    {
        if (string.IsNullOrEmpty(s))
            throw new ArgumentException("param can not be null or empty");
    }
}
