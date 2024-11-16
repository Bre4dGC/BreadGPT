using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BreadGPT.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private ChatViewModel _selectedChat;
        public ChatViewModel SelectedChat
        {
            get => _selectedChat;
            set => SetProperty(ref _selectedChat, value);
        }

        public ObservableCollection<ChatViewModel> Chats { get; } = new();

        public ICommand CreateChatCommand { get; }
        public ICommand DeleteChatCommand { get; }

        public MainViewModel()
        {
            Chats.Add(new ChatViewModel { Id = Guid.NewGuid(), Title = "Новый чат", LastMessageAt = DateTime.Now });

            CreateChatCommand = new RelayCommand(CreateChat);
            DeleteChatCommand = new RelayCommand(DeleteChat);
        }

        private void LoadChats()
        {
            // Здесь подгрузка чатов из базы данных или API
            Chats.Add(new ChatViewModel { Id = Guid.NewGuid(), Title = "Работа", LastMessageAt = DateTime.Now });
            Chats.Add(new ChatViewModel { Id = Guid.NewGuid(), Title = "Идеи", LastMessageAt = DateTime.Now.AddDays(-1) });
        }

        private void CreateChat()
        {
            var newChat = new ChatViewModel
            {
                Id = Guid.NewGuid(),
                Title = $"Новый чат {Chats.Count + 1}",
                LastMessageAt = DateTime.UtcNow
            };

            //SortChats();
            Chats.Insert(0, newChat);
            SelectedChat = newChat;
        }

        private void DeleteChat()
        {

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
    }
}
