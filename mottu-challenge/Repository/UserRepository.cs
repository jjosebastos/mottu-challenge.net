using Microsoft.EntityFrameworkCore;
using mottu_challenge.Connection;
using mottu_challenge.Model;

namespace mottu_challenge.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }
        public async Task<string> GetUserRoleAsync(int userId)
        {
            return await _context.Users
                .Where(ur => ur.IdUser == userId)
                .Select(ur => ur.Role.RoleName)
                .FirstOrDefaultAsync(); 
        }

    }
}