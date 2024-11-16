namespace BreadGPT.Models
{
    public class TokenUsage
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int TokensUsed { get; set; }

        public virtual Chat Chat { get; set; }
    }
}
