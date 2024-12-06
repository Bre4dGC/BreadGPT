using System.Collections.ObjectModel;

namespace BreadGPT.Models
{
    public class Chat : DomainObject
    {
        public string Title { get; set; } = string.Empty;
        public DateTime LastMessageAt { get; set; }
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
    }
}
