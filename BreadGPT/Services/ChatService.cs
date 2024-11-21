using BreadGPT.Models;

namespace BreadGPT.Services
{
    internal class ChatService : IChatService
    {
        public Task CreateAsync(Chat chat)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Chat>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, Chat chat)
        {
            throw new NotImplementedException();
        }
    }
}
