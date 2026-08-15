using FinSight.Usuario.Domain.Entities;
using FinSight.Usuario.Domain.Interfaces.IRepositories;
using FinSight.Usuario.Domain.Interfaces.IServices;

namespace FinSight.Usuario.Application.Services
{
    public class EnderecoService : IEnderecoService
    {

        private readonly IEnderecoRepository _repo;

        public EnderecoService(IEnderecoRepository repo)
        {
            _repo = repo;
        }

        public async Task AddAdress(string email, EnderecoDTO endereco)
        {
            await _repo.AddAdress(email, endereco);
        }

        public async Task RemoveAdress(string email, int id)
        {
            await _repo.RemoveAdress(email, id);
        }

        public async Task SetAdress(string email, int id, EnderecoDTO dto)
        {
            await _repo.SetAdress(email, id, dto);
        }
    }
}
