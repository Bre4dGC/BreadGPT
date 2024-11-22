using BreadGPT.Data;
using BreadGPT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BreadGPT.Services
{
    internal class ChatService<T> : IChatService<T> where T : DomainObject 
    {
        private readonly BreadgptDbContextFactory _contextFactory;

        public ChatService(BreadgptDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> CreateAsync(T chat)
        {
            using (BreadGPTDbContext context = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> CreatedChat = await context.Set<T>().AddAsync(chat);
                await context.SaveChangesAsync();

                return CreatedChat.Entity;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (BreadGPTDbContext context = _contextFactory.CreateDbContext())
            {
                T chat = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(chat);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (BreadGPTDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> chats = await context.Set<T>().ToListAsync();
                return chats;
            }
        }

        public async Task<T> GetAsync(Guid id)
        {
            using (BreadGPTDbContext context = _contextFactory.CreateDbContext())
            {
                T chat = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                return chat;
            }
        }

        public async Task<T> UpdateAsync(Guid id, T chat)
        {
            using (BreadGPTDbContext context = _contextFactory.CreateDbContext())
            {
                chat.Id = id;
                context.Set<T>().Update(chat);
                await context.SaveChangesAsync();

                return chat;
            }
        }
    }
}
