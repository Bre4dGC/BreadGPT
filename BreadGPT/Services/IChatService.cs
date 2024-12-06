using BreadGPT.Models;

namespace BreadGPT.Services
{
    interface IChatService
    {
        Task<IEnumerable<Chat>> GetAll();
        Task<Chat> Create(Chat chat);
        Task<Chat> Update(Guid id, Chat chat);
        Task<bool> Delete(Guid id);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
    }
}
