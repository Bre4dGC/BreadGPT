namespace BreadGPT.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public string Text { get; set; } = string.Empty;
        public MessageSender Sender { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public MessageStatus Status { get; set; }

        public virtual Chat Chat { get; set; }
    }
}
