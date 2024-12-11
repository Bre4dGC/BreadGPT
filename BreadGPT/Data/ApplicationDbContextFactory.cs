using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using System.Windows;

namespace BreadGPT.Data
{
    internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args = null)
        {
            string appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "BreadGPT",
                "App.db"
            );

            if (!File.Exists(appDataPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(appDataPath));
                File.Copy("bin/App.db", appDataPath);
            }

            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlite($"Data Source={appDataPath};");

            return new ApplicationDbContext(options.Options);
        }
    }
}
