using System.Collections.Generic;

namespace PocHealthcheck.Logging.Serilog.AspNetCore
{
    public class MyLoggerFactoryOptions
    {
        public ICollection<MyLoggerRegistration> Registrations { get; } = new List<MyLoggerRegistration>();
    }
}
