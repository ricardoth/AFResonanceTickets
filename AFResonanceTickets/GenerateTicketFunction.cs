using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AFResonanceTickets
{
    public class GenerateTicketFunction
    {
        [FunctionName("GenerateTicketFunction")]
        public void Run([QueueTrigger("resonance-ticket-queue", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            //Mensaje Prueba {"preferenceID":"437710298-506b68c4-90c3-4de6-9b65-2019fb76fb01"}
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
