using BreadGPT.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Input;

namespace BreadGPT.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        public Chat CurrentChat { get; set; }
        public ObservableCollection<Message> Messages { get; } = new();

        private string _newMessage;
        public string NewMessage
        {
            get { return _newMessage; }
            set { _newMessage = value; OnPropertyChanged(); }
        }

        public ICommand SendMessageCommand { get; }

        public ChatViewModel(Chat chat)
        {
            CurrentChat = chat;
            SendMessageCommand = new RelayCommand(SendMessage);
        }

        private async void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(NewMessage)) return;

            Messages.Add(
                new Message
                {
                    Text = NewMessage,
                    Sender = MessageSender.User
                });

            NewMessage = string.Empty;

            await GetResponse(NewMessage);
        }

        private async Task GetResponse(string message)
        {
            var json = JsonSerializer.Serialize(new
            {
                model = "mistral-large-latest",
                messages = new { role = "user", content = message }
            });

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.mistral.ai/v1/chat/completions");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer r15uvSJsdSTGKygysJWuJqKRZjQGAKEm");

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, (string)null!) { Content = content });


            Messages.Add(
                new Message
                {
                    Text = await responseMessage.Content.ReadAsStringAsync(),
                    Sender = MessageSender.ChatGPT
                });
        }
    }
}
