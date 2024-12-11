using BreadGPT.Data;
using BreadGPT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BreadGPT.Services
{
    internal class ChatService : IChatService
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public ChatService(ApplicationDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Chat> Create(Chat chat)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                EntityEntry<Chat> CreatedChat = await context.Set<Chat>().AddAsync(chat);
                await context.SaveChangesAsync();

                return CreatedChat.Entity;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Chat chat = await context.Set<Chat>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<Chat>().Remove(chat);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<IEnumerable<Chat>> GetAll()
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Chat> chats = await context.Set<Chat>().ToListAsync();
                return chats;
            }
        }

        public async Task<Chat> Get(Guid id)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Chat chat = await context.Set<Chat>().FirstOrDefaultAsync((e) => e.Id == id);
                return chat;
            }
        }

        public async Task<Chat> Update(Guid id, Chat chat)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                chat.Id = id;
                context.Set<Chat>().Update(chat);
                await context.SaveChangesAsync();

                return chat;
            }
        }
    }
}
