using mottu_challenge.Model;

namespace mottu_challenge.Repository
{
    public interface IUserRepository
    {
        // Precisamos de um método para buscar o usuário pelo nome (ou email/CNPJ)
        public Task<User> GetByUsernameAsync(string username);

        // E um método para buscar as roles (perfis) dele
        public Task<string> GetUserRoleAsync(int userId);
    }
}
