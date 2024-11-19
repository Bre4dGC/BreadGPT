namespace BreadGPT.Models
{
    public class ChatSettings
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public float Temperature { get; set; } = 0.7f;
        public int MaxTokens { get; set; } = 1000;
        public string Model { get; set; } = "gpt-3.5-turbo";

        public virtual Chat Chat { get; set; }
    }
}
