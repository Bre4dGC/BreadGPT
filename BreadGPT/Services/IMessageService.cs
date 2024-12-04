using BreadGPT.Models;

namespace BreadGPT.Services
{
    internal interface IMessageService
    {
        Task<IEnumerable<Message>> GetAllAsync();
        Task<Message> SendAsync(Message message);
    }
}
