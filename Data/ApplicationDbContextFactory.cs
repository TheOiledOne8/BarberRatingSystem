using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BarberRatingSystem.Data
{
    public class ApplicationDbContextFactory
        : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Start where EF is running
            var basePath = Directory.GetCurrentDirectory();

            // Walk up until we find appsettings.json (or hit root)
            while (!File.Exists(Path.Combine(basePath, "appsettings.json"))
                   && basePath != Directory.GetDirectoryRoot(basePath))
            {
                basePath = Directory.GetParent(basePath).FullName;
            }

            var configPath = Path.Combine(basePath, "appsettings.json");
            if (!File.Exists(configPath))
                throw new FileNotFoundException(
                  $"Could not find appsettings.json at {configPath}");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}