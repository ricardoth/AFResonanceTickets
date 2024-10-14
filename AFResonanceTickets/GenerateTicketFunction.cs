using AFResonanceTickets.Application.Commands.Preferences;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AFResonanceTickets
{
    public class GenerateTicketFunction
    {
        private readonly IMediator _mediator;

        public GenerateTicketFunction(IMediator mediator)
        {
            _mediator = mediator;                
        }

        [FunctionName("GenerateTicketFunction")]
        public async Task Run([QueueTrigger("resonance-ticket-queue", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            //Mensaje Prueba {"preferenceID":"437710298-506b68c4-90c3-4de6-9b65-2019fb76fb01"}
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            try
            {
                var response = await _mediator.Send(new PreferenceCommandQuery(myQueueItem));

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
