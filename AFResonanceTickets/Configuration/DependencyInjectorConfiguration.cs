using AFResonanceTickets.Application.Commands.Preferences;
using AFResonanceTickets.Domain.ValueObjects;
using AFResonanceTickets.Integrations.Interfaces;
using AFResonanceTickets.Integrations.Services;
using MediatR;
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

            #region Mediator
            service.AddTransient<IRequestHandler<PreferenceCommandQuery, bool>, PreferenceCommandHandler>();
            #endregion

            service.AddTransient<IPreferenceService, PreferenceService>();
        }
    }
}