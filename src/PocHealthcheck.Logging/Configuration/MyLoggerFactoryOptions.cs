using System.Collections.Generic;

namespace PocHealthcheck.Logging.Configuration
{
    public class MyLoggerFactoryOptions
    {
        public ICollection<MyLoggerProviderRegistration> Registrations { get; } = new List<MyLoggerProviderRegistration>();
    }
}
