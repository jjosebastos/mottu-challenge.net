using mottu_challenge.Model;

namespace mottu_challenge.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user, string roleName);
    }
}
