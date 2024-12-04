using BreadGPT.Data;
using BreadGPT.Models;
using BreadGPT.Services;
using CommunityToolkit.Mvvm.Input;
using Mistral;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BreadGPT.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private IChatService _chatService = new ChatService(new BreadgptDbContextFactory());
        private IMessageService _messageService = new MessageService(new BreadgptDbContextFactory());
        private MistralClient _mistralClient = new MistralClient("r15uvSJsdSTGKygysJWuJqKRZjQGAKEm");

        public ObservableCollection<Chat> Chats { get; } = new ObservableCollection<Chat>();
        public ObservableCollection<Message> Messages { get; } = new ObservableCollection<Message>();

        private Chat _selectedChat;
        public Chat SelectedChat
        {
            get { return _selectedChat; }
            set { _selectedChat = value; OnPropertyChanged(); }
        }

        private string _newMessage;
        public string TextMessage
        {
            get { return _newMessage; }
            set { _newMessage = value; OnPropertyChanged(); }
        }

        public ICommand CreateChatCommand { get; }
        public ICommand RenameChatCommand { get; }
        public ICommand DeleteChatCommand { get; }
        public ICommand SelectChatCommand { get; }
        public ICommand SendMessageCommand { get; }

        public MainViewModel()
        {
            CreateChatCommand = new RelayCommand(CreateChat);
            RenameChatCommand = new RelayCommand(RenameChat);
            DeleteChatCommand = new RelayCommand(DeleteChat);
            SendMessageCommand = new RelayCommand(SendMessage);

            LoadChats();
        }

        private void CreateChat()
        {
            if (Chats.Count == 10) { MessageBox.Show("You have increased the maximum number of chats"); return; }

            //Message msg = new Message
            //{
            //    Id = Guid.NewGuid(),
            //    Text = "Text",
            //    Sender = MessageSender.User,
            //};

            //var list = new List<Message> { msg };

            _chatService.Create(
                SelectedChat = new Chat
                {
                    Id = Guid.NewGuid(),
                    Title = $"New Chat {Chats.Count + 1}",
                    CreatedAt = DateTime.UtcNow,
                    LastMessageAt = DateTime.UtcNow
                });            

            Chats.Add(SelectedChat);
            SortChats();
        }

        private async void LoadChats()
        {
            IEnumerable<Chat> chats = await _chatService.GetAll();

            if (chats.Count() == 0) { CreateChat(); return; };

            foreach (var chat in chats)
            {
                Chats.Add(chat);
            }

            SortChats();
        }

        private void SortChats()
        {
            var sorted = Chats.OrderByDescending(chat => chat.LastMessageAt).ToList();
            Chats.Clear();

            foreach (var chat in sorted)
            {
                Chats.Add(chat);
            }
        }

        private void RenameChat()
        {

        }

        private void DeleteChat()
        {

        }

        private async Task LoadMessages()
        {
            IEnumerable<Message> messages = await _messageService.GetAllAsync();

            if (messages.Count() == 0) return;

            foreach (var message in messages)
            {
                SelectedChat.Messages.Add(message);
            }
        }

        private async void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(TextMessage)) return;

            var message = TextMessage;
            TextMessage = string.Empty;

            Message msg = new Message
            {
                Id = Guid.NewGuid(),
                Text = message,
                IsSendByUser = true,
            };

            Messages.Add(msg);

            await GetResponse(message);
        }

        private async Task GetResponse(string message)
        {
            Message msg = new Message
            {
                Id = Guid.NewGuid(),
                Text = await PostRequest(message),
                IsSendByUser = false,
            };

            Messages.Add(msg);
        }

        private async Task<string> PostRequest(string message)
        {
            var answer = await _mistralClient.CompleteAsync(
                    new()
                    {
                        Model = "mistral-large-latest",
                        Messages = [new() { Role = "user", Content = message }]
                    });

            return answer.Choices[0].Message.Content;
        }
    }
}
