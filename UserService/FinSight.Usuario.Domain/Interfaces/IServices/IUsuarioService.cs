using FinSight.Usuario.Domain.Entities;

namespace FinSight.Usuario.Domain.Interfaces.IServices
{
    public interface IUsuarioService
    {
        public Task Register(RegisterModel register);
        public Task<UsuarioModel> FindByEmail(string email);
        public Task SetName(string email, string name);
        public Task SetEmail(string emailAtual, string emailAtualizado);
        public Task SetPassword(string email, string password, string confirmPas);
        public Task DeleteUser(string email);
        public Task<LoginResponse> Login(LoginModel login);
    }
}
