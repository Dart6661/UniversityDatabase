using DatabaseContext.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatabaseContext.Context
{
    public static class DbContextOptionsFactory
    {
        public static DbContextOptions<T> Create<T>() where T : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            optionsBuilder.UseNpgsql(DbConfig.GetConnectionString());
            //optionsBuilder.EnableDetailedErrors();
            //optionsBuilder.EnableSensitiveDataLogging();
            //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            return optionsBuilder.Options;
        }
    }
}
