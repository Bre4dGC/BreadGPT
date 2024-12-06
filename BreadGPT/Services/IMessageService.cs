using BreadGPT.Models;

namespace BreadGPT.Services
{
    internal interface IMessageService
    {
        Task<IEnumerable<Message>> GetAllAsync(Chat chat);
        Task<Message> SendAsync(Message message);
    }
}
