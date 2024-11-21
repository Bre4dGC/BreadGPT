using BreadGPT.Models;

namespace BreadGPT.Services
{
    internal interface IMessageService
    {
        Task<IEnumerable<Message>> GetAllAsync(Guid chatId);
        Task SendAsync(Guid chatId, string message);
    }
}
