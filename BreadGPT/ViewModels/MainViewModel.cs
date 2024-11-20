using BreadGPT.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BreadGPT.ViewModels
{
    class MainViewModel : BaseViewModel
    {
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
            Chats.Add(new Chat { Id = Guid.NewGuid(), Title = "New Chat", LastMessageAt = DateTime.Now });

            CreateChatCommand = new RelayCommand(CreateChat);

            RenameChatCommand = new RelayCommand(DeleteChat);
            DeleteChatCommand = new RelayCommand(DeleteChat);
        }

        private void LoadChats()
        {
            // Здесь подгрузка чатов из базы данных или API
            Chats.Add(new Chat { Id = Guid.NewGuid(), Title = "Работа", LastMessageAt = DateTime.Now });
            Chats.Add(new Chat { Id = Guid.NewGuid(), Title = "Идеи", LastMessageAt = DateTime.Now.AddDays(-1) });
        }

        private void CreateChat()
        {
            var newChat = new Chat
            {
                Id = Guid.NewGuid(),
                Title = $"New Chat {Chats.Count + 1}",
                LastMessageAt = DateTime.UtcNow
            };

            Chats.Insert(0, newChat);
            SelectedChat = newChat;
        }

        private void DeleteChat()
        {
            
        }
    }
}
