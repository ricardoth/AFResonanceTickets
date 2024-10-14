using AFResonanceTickets.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AFResonanceTickets.Configuration
{
    public static class DependencyInjectorConfiguration
    {
        public static void AddDependencyInjectorConfiguration(this IServiceCollection service, IConfiguration configuration)
        { 
            DecimatioSettings decimatioSettings = configuration.GetSection(DecimatioSettings.SettingsName).Get<DecimatioSettings>();
            service.AddSingleton(decimatioSettings);
        }
    }
}
