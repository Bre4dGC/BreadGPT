namespace BreadGPT.Models
{
    public class Message : DomainObject
    {
        public Guid ChatId { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsSendByUser { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
