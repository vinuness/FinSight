using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSight.Web.Models.Usuario;

namespace FinSight.Web.Interfaces
{
    public interface IUserService
    {
        public Task Cadastrar(RegisterModel register);
        public Task<LoginResponse> Login(LoginModel login);
        public Task<Usuario> FindId(Guid id);
        public Task<Usuario> FindEmail(string email);
        public Task SetEmail(string emailAtual, string emailAtualizado);
        public Task SetNome(string email, string name);
        public Task SetPass(string email, UpdatePass pass);
    }
}