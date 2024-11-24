using BreadGPT.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BreadGPT.ViewModels
{
    internal class ChatViewModel : BaseViewModel
    {
        public Chat CurrentChat { get; set; }
        public ObservableCollection<Message> Messages { get; } = new();

        private string _newMessage;
        public string NewMessage
        {
            get { return _newMessage; }
            set { _newMessage = value; OnPropertyChanged(); }
        }

        public ICommand SendMessageCommand { get; }

        public ChatViewModel()
        {
            SendMessageCommand = new RelayCommand(SendMessage);
        }

        private void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(NewMessage)) return;

            Messages.Add(
                new Message
                {
                    Text = NewMessage,
                    Sender = MessageSender.User
                });
        }
    }
}
