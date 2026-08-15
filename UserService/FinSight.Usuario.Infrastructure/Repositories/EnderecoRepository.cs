using FinSight.Usuario.Domain.Entities;
using FinSight.Usuario.Domain.Interfaces.IRepositories;
using FinSight.Usuario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinSight.Usuario.Infrastructure.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly AppDbContext _con;

        public EnderecoRepository(AppDbContext con)
        {
            _con = con;
        }

        public async Task AddAdress(string email, EnderecoDTO endereco)
        {
            var usuario = await _con.Usuarios
                .Include(u => u.Enderecos)
                .FirstOrDefaultAsync(u => u.Email == email);

            usuario.Enderecos.Add(new EnderecoModel
            {
                CEP = endereco.CEP,
                Bairro = endereco.Bairro,
                Rua = endereco.Rua,
                Numero = endereco.Numero,
                Localidade = endereco.Localidade,
                Estado = endereco.Estado,
            });

            await _con.SaveChangesAsync();
        }

        public async Task RemoveAdress(string email, int id)
        {
            var usuario = await _con.Usuarios
                .Include(e => e.Enderecos)
                .FirstOrDefaultAsync(e => e.Email == email);

            foreach (var endereco in usuario.Enderecos)
            {
                if (endereco.Id.Equals(id))
                {
                    _con.Enderecos.Remove(endereco);
                }
            }

            await _con.SaveChangesAsync();
        }

        public async Task SetAdress(string email, int id, EnderecoDTO dto)
        {
            var usuario = await _con.Usuarios
                .Include(e => e.Enderecos)
                .FirstOrDefaultAsync(e => e.Email == email);

            foreach(var endereco in usuario.Enderecos)
            {
                if (endereco.Id.Equals(id))
                {
                    endereco.CEP = dto.CEP;
                    endereco.Bairro = dto.Bairro;
                    endereco.Rua = dto.Rua;
                    endereco.Numero = dto.Numero;
                    endereco.Localidade = dto.Localidade;
                    endereco.Estado = dto.Estado;
                }
            }

            await _con.SaveChangesAsync();
        }
    }
}
