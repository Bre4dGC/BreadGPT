using BreadGPT.Data;
using BreadGPT.Models;
using BreadGPT.Services;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BreadGPT.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private IChatService<Chat> chatService = new ChatService<Chat>(new BreadgptDbContextFactory());

        private Chat _selectedChat;

        public Chat SelectedChat
        {
            get { return _selectedChat; }
            set { _selectedChat = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Chat> Chats { get; } = new();

        public ICommand CreateChatCommand { get; }
        public ICommand RenameChatCommand { get; }
        public ICommand DeleteChatCommand { get; }

        public MainViewModel()
        {
            CreateChatCommand = new RelayCommand(CreateChat);

            RenameChatCommand = new RelayCommand(DeleteChat);
            DeleteChatCommand = new RelayCommand(DeleteChat);
        }

        private void LoadChats()
        {
            var chats = chatService.GetAllAsync();
        }

        private void CreateChat()
        {
            chatService.CreateAsync(
                new Chat 
                {
                    Id = Guid.NewGuid(),
                    Title = $"New Chat",
                    LastMessageAt = DateTime.UtcNow
                });
        }

        private void DeleteChat()
        {

        }
    }
}
