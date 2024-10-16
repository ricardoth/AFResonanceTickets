using Microsoft.ApplicationInsights;
using Newtonsoft.Json.Linq;

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
                _telemetryClient.TrackTrace($"[INFO] Llamada a endpoint POST api/Ticket/TicketQueue");
                var json = JObject.Parse(preferenceTicket.PreferenceCode);
                var preferenceId = json["preferenceID"]?.ToString();
                preferenceTicket.PreferenceCode = preferenceId;
                var request = await Url.AppendPathSegments("Ticket", "TicketQueue")
                    .AllowHttpStatus()
                    .PostJsonAsync(preferenceTicket);

                if (!request.ResponseMessage.IsSuccessStatusCode)
                    throw new Exception("No se pudo generar los tickets");

                var response = await request.GetStringAsync();
                _telemetryClient.TrackTrace($"[INFO] Llamada a endpoint POST api/TicketQueue - Exitoso");
            }
            catch (FlurlHttpException ex)
            {
                _telemetryClient.TrackTrace($"[ERROR] No se pudo conectar al servicio POST api/TicketQueue: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackTrace($"[ERROR] Error inesperado: {ex.Message}");
                throw;
            }
        }
    }
}
