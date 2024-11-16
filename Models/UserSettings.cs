namespace BreadGPT.Models
{
    public class UserSettings
    {
        public Guid Id { get; set; }
        public string ApiKey { get; set; } = string.Empty;
        public bool IsSidebarHidden { get; set; }
    }
}
