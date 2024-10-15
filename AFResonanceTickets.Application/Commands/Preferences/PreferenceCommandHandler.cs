using Microsoft.ApplicationInsights;

namespace AFResonanceTickets.Application.Commands.Preferences
{
    public class PreferenceCommandHandler : IRequestHandler<PreferenceCommandQuery, bool>
    {
        private readonly IPreferenceService _preferenceService;
        private readonly TelemetryClient _telemetryClient;

        public PreferenceCommandHandler(IPreferenceService preferenceService, TelemetryClient telemetryClient)
        {
            _preferenceService = preferenceService;        
            _telemetryClient = telemetryClient;
        }

        public Task<bool> Handle(PreferenceCommandQuery request, CancellationToken cancellationToken)
        {
            _telemetryClient.TrackTrace($"[INFO] Mensaje validado correctamente desde Azure Queue {request.PreferenceCode}");
            var preferenceTicket = new PreferenceTicket()
            {
                PreferenceCode = request.PreferenceCode,
            };
            _preferenceService.GenerateTickets(preferenceTicket);
            return Task.FromResult(true);
        }
    }
}
