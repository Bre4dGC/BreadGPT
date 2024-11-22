using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Windows;

namespace BreadGPT.Data
{
    internal class BreadgptDbContextFactory : IDesignTimeDbContextFactory<BreadGPTDbContext>
    {
        public BreadGPTDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<BreadGPTDbContext>();
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BreadGPTDB;Trusted_Connection=true;");

            return new BreadGPTDbContext(options.Options);
        }
    }
}
