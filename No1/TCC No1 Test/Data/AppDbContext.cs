using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Pkcs;
using TCC_No1_Test.Entities;

namespace TCC_No1_Test.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}
