using BreadGPT.Commands;
using BreadGPT.Data;
using BreadGPT.Models;
using BreadGPT.Services;
using BreadGPT.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BreadGPT.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private Chat _сhat;

        private IChatService<Chat> chatService = new ChatService<Chat>(new BreadgptDbContextFactory());
        public ObservableCollection<Chat> Chats { get; }

        private readonly NavigationService _navigationService;
        public ICommand NavigateToChatCommand { get; }

        public ICommand CreateChatCommand { get; }
        public ICommand RenameChatCommand { get; }
        public ICommand DeleteChatCommand { get; }
        public ICommand SelectChatCommand { get; }

        public MainViewModel(NavigationService navigationService)
        {
            Chats = new ObservableCollection<Chat>();
            _navigationService = navigationService;

            CreateChatCommand = new RelayCommand(CreateChat);
            RenameChatCommand = new RelayCommand(RenameChat);
            DeleteChatCommand = new RelayCommand(DeleteChat);

            NavigateToChatCommand = new Commands.RelayCommand<ChatViewModel>(chat =>
            {
                if (chat != null)
                {
                    var chatPage = new ChatView
                    {
                        DataContext = chat
                    };

                    _navigationService.Navigate(chatPage);
                }
            });

            LoadChats();
        }

        private void CreateChat()
        {
            //if (_сhat.Messages.Count == 0) return;
            if (Chats.Count == 10) { MessageBox.Show("You have increased the maximum number of chats"); return; }

            chatService.CreateAsync(
                _сhat = new Chat
                {
                    Id = Guid.NewGuid(),
                    Title = $"New Chat {Chats.Count + 1}",
                    LastMessageAt = DateTime.UtcNow
                });

            Chats.Add(_сhat);

            SortChats();
        }

        private async void LoadChats()
        {
            IEnumerable<Chat> chats = await chatService.GetAllAsync();

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
    }
}
