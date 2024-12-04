namespace BreadGPT.Models
{
    public class Chat : DomainObject
    {
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime LastMessageAt { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
