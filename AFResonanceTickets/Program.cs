using AFResonanceTickets;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Program))]

namespace AFResonanceTickets
{
    public class Program : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfiguration configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            builder.Services.AddMediatR(typeof(Program).Assembly);
            DecimatioSettings decimatioSettings = configuration.GetSection(DecimatioSettings.SettingsName).Get<DecimatioSettings>();
            if (decimatioSettings == null)
            {
                throw new Exception("DecimatioSettings no se ha configurado correctamente.");
            }
            builder.Services.AddSingleton(decimatioSettings);

            #region Mediator
            builder.Services.AddScoped<IRequestHandler<PreferenceCommandQuery, bool>, PreferenceCommandHandler>();
            #endregion

            builder.Services.AddScoped<IPreferenceService, PreferenceService>();
        }
    }
}
