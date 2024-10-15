namespace AFResonanceTickets.Configuration
{
    public static class DependencyInjectorConfiguration
    {
        public static void AddDependencyInjectorConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            DecimatioSettings decimatioSettings = configuration.GetSection(DecimatioSettings.SettingsName).Get<DecimatioSettings>();
            service.AddSingleton(decimatioSettings);

            #region Mediator
            service.AddScoped<IRequestHandler<PreferenceCommandQuery, bool>, PreferenceCommandHandler>();
            #endregion

            service.AddScoped<IPreferenceService, PreferenceService>();
        }
    }
}