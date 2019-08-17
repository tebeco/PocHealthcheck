using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace PocHealthcheck.Logging
{
    public interface IMyLoggerProvider : ILoggerProvider
    {
        string Name { get; }
    }
}
