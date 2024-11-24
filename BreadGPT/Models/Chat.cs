namespace BreadGPT.Models
{
    public class Chat : DomainObject
    {
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastMessageAt { get; set; }
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
