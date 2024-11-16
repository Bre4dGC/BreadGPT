using BreadGPT.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BreadGPT.ViewModels
{
    class ChatViewModel : BaseViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime? LastMessageAt { get; set; }

        public ObservableCollection<MessageViewModel> Messages { get; } = new();

        private string _newMessage;
        public string NewMessage
        {
            get => _newMessage;
            set => SetProperty(ref _newMessage, value);
        }

        public ICommand SendMessageCommand { get; }

        public ChatViewModel()
        {
            SendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);
        }

        private void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(NewMessage)) return;

            // Добавляем сообщение в список
            Messages.Add(new MessageViewModel
            {
                Content = NewMessage,
                Timestamp = DateTime.Now,
                IsSentByUser = true
            });

            // Обновляем время последнего сообщения
            LastMessageAt = DateTime.Now;

            // Очищаем текстовое поле
            NewMessage = string.Empty;
        }

        private bool CanSendMessage()
        {
            return !string.IsNullOrWhiteSpace(NewMessage);
        }
    }
}
