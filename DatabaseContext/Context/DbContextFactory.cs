using Microsoft.EntityFrameworkCore.Design;

namespace DatabaseContext.Context
{
    public class UnivContextFactory : IDesignTimeDbContextFactory<UnivContext>
    {
        public UnivContext CreateDbContext(string[] args) => new(DbContextOptionsFactory.Create<UnivContext>());
    }
}
