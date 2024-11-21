using BreadGPT.Models;
using Microsoft.EntityFrameworkCore;

namespace BreadGPT.Data
{
    class BreadGPTDbContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=BreadgptDB;Integrated Security=True;Trust Server Certificate=True");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
