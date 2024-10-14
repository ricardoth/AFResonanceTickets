using AFResonanceTickets.Domain.Entities;
using AFResonanceTickets.Integrations.Interfaces;
using MediatR;

namespace AFResonanceTickets.Application.Commands.Preferences
{
    public class PreferenceCommandHandler : IRequestHandler<PreferenceCommandQuery, bool>
    {
        private readonly IPreferenceService _preferenceService;

        public PreferenceCommandHandler(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;        
        }

        public Task<bool> Handle(PreferenceCommandQuery request, CancellationToken cancellationToken)
        {
            var preferenceTicket = new PreferenceTicket()
            {
                PreferenceCode = request.PreferenceCode,
            };
            _preferenceService.GenerateTickets(preferenceTicket);
            return Task.FromResult(true);
        }
    }
}
