using FinSight.Usuario.Domain.Entities;

namespace FinSight.Usuario.Domain.Interfaces.IServices
{
    public interface IEnderecoService
    {
        public Task AddAdress(string email, EnderecoDTO endereco);
        public Task RemoveAdress(string email, int id);
        public Task SetAdress(string email, int id, EnderecoDTO dto);
    }
}
