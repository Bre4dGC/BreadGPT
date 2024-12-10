using BreadGPT.Models;

namespace BreadGPT.Services
{
    internal interface IMessageService
    {
        Task<IEnumerable<Message>> GetAll(Chat chat);
        Task<Message> Send(Message message);
    }
}
