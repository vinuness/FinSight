using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSight.Web.Interfaces;
using FinSight.Web.Models.Usuario;

namespace FinSight.Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task Cadastrar(RegisterModel register)
        {
            await _http.PostAsJsonAsync("register/user", register);
        }

        public async Task<Usuario> FindEmail(string email)
        {
            Usuario usuario = await _http.GetFromJsonAsync<Usuario>($"find/user/email/{email}");
            return usuario;
        }

        public async Task<Usuario?> FindId(Guid id)
        {
            Usuario? usuario = await _http.GetFromJsonAsync<Usuario>($"find/user/{id}");
            return usuario;
        }

        public async Task<LoginResponse?> Login(LoginModel login)
        {
            var res = await _http.PostAsJsonAsync("log/in/user", login);
            if(!res.IsSuccessStatusCode) return null;
            return await res.Content.ReadFromJsonAsync<LoginResponse>();
        }

        public async Task SetEmail(string emailAtual, string emailAtualizado)
        {
            await _http.PutAsJsonAsync($"set/email/{emailAtual}", emailAtualizado);
        }

        public async Task SetNome(string email, string name)
        {
            await _http.PutAsJsonAsync($"set/name/{email}", name);
        }

        public async Task SetPass(string email, UpdatePass pass)
        {
            await _http.PutAsJsonAsync($"set/pass/{email}", pass);
        }
    }
}