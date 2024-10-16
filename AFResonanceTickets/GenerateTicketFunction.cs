using Microsoft.ApplicationInsights;

namespace AFResonanceTickets
{
    public class GenerateTicketFunction
    {
        private readonly IMediator _mediator;
        private readonly TelemetryClient _telemetryClient;

        public GenerateTicketFunction(IMediator mediator, TelemetryClient telemetryClient)
        {
            _mediator = mediator;                
            _telemetryClient = telemetryClient;
        }

        [FunctionName("GenerateTicketFunction")]
        public async Task Run([QueueTrigger("resonance-ticket-queue", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            _telemetryClient.TrackTrace($"[INFO] Mensaje recibido desde Azure Queue resonance-ticket-queue {myQueueItem}");
            try
            {
                if (myQueueItem != null && myQueueItem != "")
                {
                    await _mediator.Send(new PreferenceCommandQuery(myQueueItem));
                }
                else
                {
                    _telemetryClient.TrackException(new System.Exception($"[ERROR] Mensaje no pasa validación {myQueueItem}"));
                }
            }
            catch (System.Exception ex)
            {
                _telemetryClient.TrackException(ex);
                throw ex;
            }
        }
    }
}
