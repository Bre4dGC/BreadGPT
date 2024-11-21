using BreadGPT.Models;

namespace BreadGPT.Services
{
    interface IChatService
    {
        Task<IEnumerable<Chat>> GetAllAsync();
        Task GetAsync(Guid id);
        Task CreateAsync(Chat chat);
        Task UpdateAsync(Guid id, Chat chat);
        Task<bool> DeleteAsync(Guid id);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
    }
}
