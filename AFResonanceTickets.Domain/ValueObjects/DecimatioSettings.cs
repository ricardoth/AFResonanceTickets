namespace AFResonanceTickets.Domain.ValueObjects
{
    public class DecimatioSettings
    {
        public const string SettingsName = "DecimatioSettings";
        public string Url { get; set; }
        public string UserBasicAuth { get; set; }
        public string PassBasicAuth { get; set; }
    }
}
