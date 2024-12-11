using BreadGPT.Data;
using BreadGPT.Models;
using Microsoft.EntityFrameworkCore;

namespace BreadGPT.Services
{
    internal class MessageService : IMessageService
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public MessageService(ApplicationDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Message>> GetAll(Chat chat)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Set<Message>().Where(m => m.ChatId == chat.Id).ToListAsync();
            }
        }

        public async Task<Message> Send(Message message)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                var createdMessage = await context.Set<Message>().AddAsync(message);
                await context.SaveChangesAsync();

                return createdMessage.Entity;
            }
        }
    }
}
