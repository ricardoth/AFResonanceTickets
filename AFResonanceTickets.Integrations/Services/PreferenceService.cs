namespace AFResonanceTickets.Integrations.Services
{
    public sealed class PreferenceService : IPreferenceService
    {
        private DecimatioSettings _config { get; }

        public PreferenceService(DecimatioSettings config)
        {
            _config = config;        
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
                var request = await Url.AppendPathSegment("ticketQueue")
                    .AllowHttpStatus()
                    .PostJsonAsync(preferenceTicket);

                if (request.StatusCode != 200)
                    throw new Exception("No se pudo generar los tickets");

                var response = await request.GetStringAsync();
                //Registrar respuesta en insight
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseStringAsync();
                throw new Exception($"Error al invocar el servicio de Generar Tickets: {error}");
            }
        }
    }
}
