using Microsoft.ApplicationInsights;

namespace AFResonanceTickets.Integrations.Services
{
    public sealed class PreferenceService : IPreferenceService
    {
        private DecimatioSettings _config { get; }
        private readonly TelemetryClient _telemetryClient;

        public PreferenceService(DecimatioSettings config, TelemetryClient telemetryClient)
        {
            _config = config;        
            _telemetryClient = telemetryClient;
        }

        private IFlurlRequest Url
        {
            get
            {
                string credentials = $"{_config.UserBasicAuth}:{_config.PassBasicAuth}";
                string encodingCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
                return _config.Url.WithHeader("Authorization", $"Basic {encodingCredentials}");
            }
        }

        public async Task GenerateTickets(PreferenceTicket preferenceTicket)
        {
            try
            { 
                _telemetryClient.TrackTrace($"[INFO] Llamada a endpoint POST api/TicketQueue");
                var request = await Url.AppendPathSegment("TicketQueue")
                    .AllowHttpStatus()
                    .PostJsonAsync(preferenceTicket);

                if (request.StatusCode != 200) 
                    throw new Exception("No se pudo generar los tickets");

                var response = await request.GetStringAsync();
                _telemetryClient.TrackTrace($"[INFO] Llamada a endpoint POST api/TicketQueue - Exitoso");
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseStringAsync();
                _telemetryClient.TrackTrace($"[ERROR] No se pudo conectar al servicio POST api/TicketQueue: {error}");
                throw new Exception($"[ERROR] No se pudo conectar al servicio POST api/TicketQueue: {error}");
            }
        }
    }
}
