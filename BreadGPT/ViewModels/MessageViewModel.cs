namespace BreadGPT.ViewModels
{
    class MessageViewModel : BaseViewModel
    {
        private string _content;
        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        private DateTime _timestamp;
        public DateTime Timestamp
        {
            get => _timestamp;
            set => SetProperty(ref _timestamp, value);
        }

        private bool _isSentByUser;
        public bool IsSentByUser
        {
            get => _isSentByUser;
            set => SetProperty(ref _isSentByUser, value);
        }
    }
}
