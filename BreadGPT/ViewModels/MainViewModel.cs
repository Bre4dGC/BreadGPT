using BreadGPT.Data;
using BreadGPT.Models;
using BreadGPT.Services;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BreadGPT.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private ChatViewModel _selectedChat;

        public ChatViewModel SelectedChat
        {
            get { return _selectedChat; }
            set { _selectedChat = value; OnPropertyChanged(); }
        }

        private IChatService<Chat> chatService = new ChatService<Chat>(new BreadgptDbContextFactory());
        public ObservableCollection<Chat> Chats { get; } = new();

        public ICommand CreateChatCommand { get; }
        public ICommand RenameChatCommand { get; }
        public ICommand DeleteChatCommand { get; }
        public ICommand SelectChatCommand { get; }

        public MainViewModel()
        {
            LoadChats();

            CreateChatCommand = new RelayCommand(CreateChat);
            RenameChatCommand = new RelayCommand(DeleteChat);
            DeleteChatCommand = new RelayCommand(DeleteChat);
        }

        private void CreateChat()
        {
            if (Chats.Count == 10) { MessageBox.Show("You have increased the maximum number of chats"); return; }

            Chat chat;
            chatService.CreateAsync(
                chat = new Chat
                {
                    Id = Guid.NewGuid(),
                    Title = $"New Chat {Chats.Count + 1}",
                    LastMessageAt = DateTime.UtcNow
                });

            Chats.Add(chat);

            SortChats();
        }

        private async void LoadChats()
        {
            var chats = await chatService.GetAllAsync();

            foreach(var chat in chats)
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

        private void DeleteChat()
        {

        }
    }
}
