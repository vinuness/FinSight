using FinSight.Usuario.Domain.Entities;

namespace FinSight.Usuario.Domain.Interfaces.IRepositories
{
    public interface IUsuarioRepository
    {
        public Task Register(RegisterModel register);
        public Task<UsuarioModel> FindByEmail(string email);
        public Task<UsuarioModel> FindById(Guid id);
        public Task SetName(string email, string name);
        public Task SetEmail(string emailAtual, string emailAtualizado);
        public Task SetPassword(string email, string password, string confirmPas);
        public Task DeleteUser(string email);
        public bool VerificarSenha(string password, string hash);
        public string GenerateToken(UsuarioModel usuario);
    }
}
