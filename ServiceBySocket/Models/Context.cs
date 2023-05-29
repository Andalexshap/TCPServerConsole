using Microsoft.EntityFrameworkCore;

namespace ServiceBySocket.Models
{
    public class Context : DbContext
    {
        public DbSet<Car> Сars { get; set; } = null!;

        public Context()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=mirtekcar;Username=admin;Password=admin");
        }
    }
}
