namespace PocHealthcheck.Logging
{
    public interface IMyLoggerProviderFactory
    {
        IMyLoggerProvider CreateProvider(MyLoggerProviderRegistration registration);
    }
}
