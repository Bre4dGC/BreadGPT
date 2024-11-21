using BreadGPT.Models;

namespace BreadGPT.Services
{
    internal class MessageService : IMessageService
    {
        public Task<IEnumerable<Message>> GetAllAsync(Guid chatId)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(Guid chatId, string message)
        {
            throw new NotImplementedException();
        }
    }
}
