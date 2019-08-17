namespace PocHealthcheck.Logging
{
    public interface IMyLoggerProviderFactory
    {
        IMyLoggerProvider CreateProvider(string name);
    }
}
