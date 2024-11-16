namespace BreadGPT.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public ICollection<Chat> Chats { get; set; } = new List<Chat>();
    }
}
