using Microsoft.Extensions.Logging;
using ZLogger;

namespace BestPracticesConsoleApp;

public sealed class MyClass(ILogger<MyClass> logger)
{
    public void Foo()
    {
        // log text.
        logger.ZLogDebug("foo{0} bar{1}", 10, 20);

        // log text with structure in Structured Logging.
        logger.ZLogDebugWithPayload(new { Foo = 10, Bar = 20 }, "foo{0} bar{1}", 10, 20);
    }
}