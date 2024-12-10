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
    public class MainViewModel : BaseViewModel
    {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;
        private readonly MistralClient _mistralClient;

        public ObservableCollection<Chat> Chats { get; } = new ObservableCollection<Chat>();

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
            _chatService = new ChatService(new BreadgptDbContextFactory());
            _messageService = new MessageService(new BreadgptDbContextFactory());
            _mistralClient = new MistralClient("r15uvSJsdSTGKygysJWuJqKRZjQGAKEm");

            CreateChatCommand = new RelayCommand(CreateChat);
            RenameChatCommand = new RelayCommand(RenameChat);
            DeleteChatCommand = new RelayCommand(DeleteChat);
            SendMessageCommand = new RelayCommand(SendMessage);

            LoadChats();
        }

        private void CreateChat()
        {
            if (Chats.Any() && SelectedChat.Messages.Count == 0) return;
            if (Chats.Count >= 10)
            {
                MessageBox.Show("You have reached the maximum number of chats.");
                return;
            }

            var newChat = new Chat
            {
                Id = Guid.NewGuid(),
                Title = $"New Chat {Chats.Count + 1}",
                LastMessageAt = DateTime.UtcNow
            };

            _chatService.Create(newChat);
            Chats.Add(newChat);
            SortChats();
            SelectedChat = newChat;
        }

        private async void LoadChats()
        {
            var chats = await _chatService.GetAll();

            if (!chats.Any())
            {
                CreateChat();
                return;
            }

            foreach (var chat in chats)
            {
                Chats.Add(chat);
                await LoadMessages(chat);
            }

            SortChats();
        }

        private async Task LoadMessages(Chat chat)
        {
            var messages = await _messageService.GetAll(chat);

            foreach (var message in messages)
            {
                chat.Messages.Add(message);
            }

            SortMessages(chat);
        }

        private void SortChats()
        {
            var sortedChats = Chats.OrderByDescending(chat => chat.LastMessageAt).ToList();
            Chats.Clear();
            foreach (var chat in sortedChats)
            {
                Chats.Add(chat);
            }
            SelectedChat = Chats.FirstOrDefault();
        }

        private void SortMessages(Chat chat)
        {
            chat.Messages = new ObservableCollection<Message>(chat.Messages.OrderBy(message => message.SentAt));
        }

        private void RenameChat()
        {
            // TODO: Implement RenameChat functionality.
        }

        private void DeleteChat()
        {
            // TODO: Implement DeleteChat functionality.
        }

        private async void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(TextMessage) || SelectedChat.Messages.Count >= 25) return;

            var messageText = TextMessage;
            TextMessage = string.Empty;

            var newMessage = new Message
            {
                Id = Guid.NewGuid(),
                Text = messageText,
                IsSendByUser = true,
                SentAt = DateTime.UtcNow,
                ChatId = SelectedChat.Id
            };

            await _messageService.Send(newMessage);

            SelectedChat.Messages.Add(newMessage);
            SelectedChat.LastMessageAt = DateTime.UtcNow;
            SortChats();

            SendButtonEnabled = false;
            await GetResponse(SelectedChat, messageText);
            SendButtonEnabled = true;
        }

        private async Task GetResponse(Chat chat, string userMessage)
        {
            var responseText = await PostRequest(userMessage);

            var responseMessage = new Message
            {
                Id = Guid.NewGuid(),
                Text = responseText,
                IsSendByUser = false,
                SentAt = DateTime.UtcNow,
                ChatId = chat.Id
            };

            chat.Messages.Add(responseMessage);
            await _messageService.Send(responseMessage);
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
                return "Something went wrong :(";
            }
        }
    }
}
