using FinSight.Usuario.Domain.Entities;
using FinSight.Usuario.Domain.Interfaces.IRepositories;
using FinSight.Usuario.Domain.Interfaces.IServices;

namespace FinSight.Usuario.Application.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _repo;
        
        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task Register(RegisterModel register)
        {
            await _repo.Register(register);
        }

        public async Task<UsuarioModel> FindByEmail(string email)
        {
            return await _repo.FindByEmail(email);
        }

        public async Task SetEmail(string emailAtual, string emailAtualizado)
        {
            await _repo.SetEmail(emailAtual, emailAtualizado);
        }

        public async Task SetName(string email, string name)
        {
            await _repo.SetName(email, name);
        }

        public async Task SetPassword(string email, string password, string confirmPas)
        {
            await _repo.SetPassword(email, password, confirmPas);
        }

        public async Task DeleteUser(string email)
        {
            await _repo.DeleteUser(email);
        }

        public async Task<LoginResponse> Login(LoginModel login)
        {
            var usuario = await _repo.FindByEmail(login.Email);

            if (usuario == null) return null;

            if (!_repo.VerificarSenha(login.Password, usuario.Senha ?? "")) return null;

            var response = new LoginResponse
            {
                Email = usuario.Email ?? "",
                Nome = usuario.Nome ?? "",
                Role = usuario.Role,
                Token = _repo.GenerateToken(usuario)
            };

            if (response == null) throw new Exception("Resposta nula");

            return response;
        }

    }
}
