using Microsoft.EntityFrameworkCore;
using mottu_challenge.Model;

namespace mottu_challenge.Connection
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserDto> Users { get; set; }

    }
}
