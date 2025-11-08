using mottu_challenge.Model;

namespace mottu_challenge.Service
{
    public interface ITokenService
    {
        string GenerateToken(User user, string roleName);
    }
}
