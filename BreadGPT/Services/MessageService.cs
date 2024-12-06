using BreadGPT.Data;
using BreadGPT.Models;
using Microsoft.EntityFrameworkCore;

namespace BreadGPT.Services
{
    internal class MessageService : IMessageService
    {
        private readonly BreadgptDbContextFactory _contextFactory;

        public MessageService(BreadgptDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Message>> GetAllAsync(Chat chat)
        {
            using (BreadGPTDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Set<Message>().Where(m => m.ChatId == chat.Id).ToListAsync();
            }
        }

        public async Task<Message> SendAsync(Message message)
        {
            using (BreadGPTDbContext context = _contextFactory.CreateDbContext())
            {
                var createdMessage = await context.Set<Message>().AddAsync(message);
                await context.SaveChangesAsync();

                return createdMessage.Entity;
            }
        }
    }
}
