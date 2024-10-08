using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AFResonanceTickets
{
    public class GenerateTicketFunction
    {
        [FunctionName("GenerateTicketFunction")]
        public void Run([QueueTrigger("resonance-ticket-queue", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
