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

        public ObservableCollection<Chat> Chats { get; set; } = new ObservableCollection<Chat>();

        private bool _sendButtonEnabled = true;
        public bool SendButtonEnabled
        {
            get { return _sendButtonEnabled; }
            set { _sendButtonEnabled = value; OnPropertyChanged(); }
        }

        private Chat _selectedChat;
        public Chat SelectedChat
        {
            get { return _selectedChat; }
            set { _selectedChat = value; OnPropertyChanged(nameof(SelectedChat)); }
        }

        private Message _message;
        public Message Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
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

        private async void LoadChats()
        {
            IEnumerable<Chat> chats = await _chatService.GetAll();

            if (chats.Count() == 0) { CreateChat(); return; };

            foreach (var chat in chats)
            {
                Chats.Add(chat);
                LoadMessages(chat);
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

        private async void LoadMessages(Chat chat)
        {
            IEnumerable<Message> messages = await _messageService.GetAllAsync(chat);

            if (messages.Count() == 0) return;

            foreach (var message in messages)
            {
                chat.Messages.Add(message);
            }
        }

        private async void SendMessage()
        {
            if(string.IsNullOrWhiteSpace(TextMessage) || SelectedChat.Messages.Count == 10) return;

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

            SendButtonEnabled = false;

            await GetResponse(message);

            SendButtonEnabled = true;
        }

        private async Task GetResponse(string message)
        {
            SelectedChat.Messages.Add(
                Message = new Message
                {
                    Id = Guid.NewGuid(),
                    Text = await PostRequest(message),
                    IsSendByUser = false,
                    ChatId = SelectedChat.Id,
                });

            await _messageService.SendAsync(Message);
        }

        private async Task<string> PostRequest(string message)
        {
            try
            {
                var answer = await _mistralClient.CompleteAsync(
                        new()
                        {
                            Model = "mistral-large-latest",
                            Messages = [new() { Role = "user", Content = message }]
                        });

                return answer;
            }
            catch
            {
                await Task.Delay(700);
                return "Something wrong :(";
            }
        }
    }
}
