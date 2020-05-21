using Microsoft.Extensions.Configuration;

namespace ConfigurationTest.Configuration
{
    public class CurrentDirectoryJsonConfigurationSource : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new CurrentDirectoryJsonFileConfigurationProvider();
    }
}
}
