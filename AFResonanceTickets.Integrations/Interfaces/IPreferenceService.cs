using AFResonanceTickets.Domain.Entities;

namespace AFResonanceTickets.Integrations.Interfaces
{
    public interface IPreferenceService
    {
        Task GenerateTickets(PreferenceTicket preferenceTicket);
    }
}
