using Microsoft.Extensions.Logging;

namespace PocHealthcheck.Logging
{
    public interface IMyLoggerProvider : ILoggerProvider
    {
        string Name { get; }
    }
}
