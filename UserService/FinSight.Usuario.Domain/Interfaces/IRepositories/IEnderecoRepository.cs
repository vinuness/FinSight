using FinSight.Usuario.Domain.Entities;

namespace FinSight.Usuario.Domain.Interfaces.IRepositories
{
    public interface IEnderecoRepository
    {
        public Task AddAdress(string email, EnderecoDTO endereco);
        public Task RemoveAdress(string email, int id);
        public Task SetAdress(string email, int id, EnderecoDTO dto);
    }
}
