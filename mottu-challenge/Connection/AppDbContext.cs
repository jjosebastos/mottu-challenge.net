using Microsoft.EntityFrameworkCore;
using mottu_challenge.Data.Mappings;
using mottu_challenge.Model;

namespace mottu_challenge.Connection
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
