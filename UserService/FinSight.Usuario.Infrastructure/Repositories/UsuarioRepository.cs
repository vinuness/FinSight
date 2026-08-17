using BCrypt.Net;
using FinSight.Usuario.Domain.Entities;
using FinSight.Usuario.Domain.Interfaces.IRepositories;
using FinSight.Usuario.Infrastructure.Data;
using FinSight.Usuario.Infrastructure.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace FinSight.Usuario.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly AppDbContext _con;
        private readonly JWTService _jwt;

        public UsuarioRepository(AppDbContext con, JWTService jwt)
        {
            _con = con;
            _jwt = jwt;
        }

        public async Task Register(RegisterModel register)
        {

            if(register.ConfirmaSenha.Equals(register.Senha)) {
                var usuario = new UsuarioModel
                {
                    Nome = register.Nome,
                    Email = register.Email,
                    Senha = HashSenha(register.Senha),
                    CPF = register.CPF
                };

                await _con.Usuarios.AddAsync(usuario);
                await _con.SaveChangesAsync();
            }
        }

        public async Task<UsuarioModel> FindById(Guid id)
        {
            try
            {
                var usuario = await _con.Usuarios
                    .Include(u => u.Enderecos)
                    .FirstOrDefaultAsync(u => u.Id == id);
                
                return usuario;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UsuarioModel> FindByEmail(string email)
        {
            var usuario = await _con.Usuarios
                .Include(u => u.Enderecos)
                .FirstOrDefaultAsync(u => u.Email == email);

            return usuario;
        }

        public async Task SetEmail(string emailAtual, string emailAtualizado)
        {
            var usuario = await _con.Usuarios
                .FirstOrDefaultAsync(u => u.Email == emailAtual);

            usuario.Email = emailAtualizado;

            await _con.SaveChangesAsync();
        }

        public async Task SetName(string email, string name)
        {
            var usuario = await _con.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            usuario.Nome = name;

            await _con.SaveChangesAsync();
        }

        public async Task SetPassword(string email, string password, string confirmPas)
        {
            var usuario = await _con.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            if (password == null) throw new Exception("Senha nula");
            if (confirmPas == null) throw new Exception("Confirmação de senha nula");
            if (!confirmPas.Equals(password)) throw new Exception("Dados inconsistentes");

            if (confirmPas.Equals(password))
            {
                usuario.Senha = password;

                await _con.SaveChangesAsync();
            }
        }

        public async Task DeleteUser(string email)
        {
            var usuario = await _con.Usuarios
                .Include(u => u.Enderecos)
                .FirstOrDefaultAsync(u => u.Email == email);

            _con.Usuarios.Remove(usuario);

            await _con.SaveChangesAsync();
        }

        public string HashSenha(string pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass);
        }

        public bool VerificarSenha(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public string GenerateToken(UsuarioModel usuario)
        {
            return _jwt.GenerateToken(usuario);
        }
    }
}
