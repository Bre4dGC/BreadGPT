﻿namespace BreadGPT.Models
{
    public class Message : DomainObject
    {
        public Guid ChatId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public MessageSender Sender { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
