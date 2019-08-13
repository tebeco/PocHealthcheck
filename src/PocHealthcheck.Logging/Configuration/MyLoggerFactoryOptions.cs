using System.Collections.Generic;

namespace PocHealthcheck.Logging.Configuration
{
    public class MyLoggerFactoryOptions
    {
        public ICollection<MyLoggerRegistration> Registrations { get; } = new List<MyLoggerRegistration>();
    }
}
