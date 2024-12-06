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

        private ObservableCollection<Chat> _chats { get; set; }
        public ObservableCollection<Chat> Chats
        {
            get { return _chats; }
            set { _chats = value; OnPropertyChanged(); }
        }

        private Chat _selectedChat;
        public Chat SelectedChat
        {
            get { return _selectedChat; }
            set { _selectedChat = value; OnPropertyChanged(); }
        }

        private Message _message;

        public Message Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _textMessage;
        public string TextMessage
        {
            get { return _textMessage; }
            set { _textMessage = value; OnPropertyChanged(); }
        }

        public ICommand CreateChatCommand { get; }
        public ICommand RenameChatCommand { get; }
        public ICommand DeleteChatCommand { get; }
        public ICommand SendMessageCommand { get; }
        public ICommand SelectChatCommand { get; }

        public MainViewModel()
        {
            _chats = new ObservableCollection<Chat>();

            CreateChatCommand = new RelayCommand(CreateChat);
            RenameChatCommand = new RelayCommand(RenameChat);
            DeleteChatCommand = new RelayCommand(DeleteChat);
            SelectChatCommand = new RelayCommand(SelectChat);
            SendMessageCommand = new RelayCommand(SendMessage);

            LoadChats();
        }

        private void CreateChat()
        {
            if (Chats.Count > 0 && SelectedChat.Messages.Count == 0) return;
            if (Chats.Count == 10) { MessageBox.Show("You have increased the maximum number of chats"); return; }

            _chatService.Create(
                SelectedChat = new Chat
                {
                    Id = Guid.NewGuid(),
                    Title = $"New Chat {Chats.Count + 1}",
                    LastMessageAt = DateTime.UtcNow
                });            

            Chats.Add(SelectedChat);
            SortChats();
        }

        private void SelectChat()
        {
            LoadMessages();
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

            SelectedChat = Chats[0];
        }

        private void RenameChat()
        {

        }

        private void DeleteChat()
        {

        }

        private async void LoadMessages()
        {
            //IEnumerable<Message> messages = await _messageService.GetAllAsync(SelectedChat);

            //if (messages.Count() == 0) return;

            //SelectedChat.Messages = (ObservableCollection<Message>)messages;

            Message = new Message
            {
                Id = Guid.NewGuid(),
                Text = "message",
                IsSendByUser = true,
                ChatId = SelectedChat.Id,
            };

            SelectedChat.Messages.Add(Message);
        }

        private async void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(TextMessage)) return;
            if (SelectedChat.Messages.Count == 10) return;

            var message = TextMessage;
            TextMessage = string.Empty;

            Message = new Message
            {
                Id = Guid.NewGuid(),
                Text = message,
                IsSendByUser = true,
                ChatId = SelectedChat.Id,
            };

            await _messageService.SendAsync(Message);

            SelectedChat.Messages.Add(Message);
            SelectedChat.LastMessageAt = DateTime.UtcNow;

            SortChats();

            await GetResponse(message);
        }

        private async Task GetResponse(string message)
        {
            Message = new Message
            {
                Id = Guid.NewGuid(),
                Text = "",
                IsSendByUser = false,
                ChatId = SelectedChat.Id,
            };

            SelectedChat.Messages.Add(Message);

            Message.Text = await PostRequest(message);

            await _messageService.SendAsync(Message);
        }

        private async Task<string> PostRequest(string message)
        {
            var answer = await _mistralClient.CompleteAsync(
                    new()
                    {
                        Model = "mistral-large-latest",
                        Messages = [new() { Role = "user", Content = message }]
                    });

            return answer;
        }
    }
}
