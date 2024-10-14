using MediatR;

namespace AFResonanceTickets.Application.Commands.Preferences
{
    public class PreferenceCommandQuery : IRequest<bool>
    {
        public PreferenceCommandQuery(string preferenceCode)
        {
            PreferenceCode = preferenceCode;                
        }

        public string PreferenceCode { get; set; }
    }
}