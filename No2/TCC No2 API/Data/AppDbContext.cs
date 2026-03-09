using Microsoft.EntityFrameworkCore;
using TCC_No2_API.Entities;

namespace TCC_No2_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserAuthen> UserAuthens { get; set; }
    }
}
