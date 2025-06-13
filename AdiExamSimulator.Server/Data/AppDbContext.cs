using Microsoft.EntityFrameworkCore;
using AdiExamSimulator.Server.Models;

namespace AdiExamSimulator.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Result> Results => Set<Result>();
    }
}
