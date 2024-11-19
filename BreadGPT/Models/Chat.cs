namespace BreadGPT.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastMessageAt { get; set; }
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
        public virtual ChatSettings Settings { get; set; }
    }
}
