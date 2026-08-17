using System.Net;
using FinSight.Metas.Domain.Entities;
using FinSight.Metas.Domain.Interfaces;
using Microsoft.Extensions.Http;

namespace FinSight.Metas.Application.Services
{
    public class MetaService : IMetaService
    {
        private readonly IMetaRepository _repo;
        private readonly HttpClient _http;
        private const string clientUrl = "https://localhost:5203/api/Usuario";

        public MetaService(IHttpClientFactory factory, IMetaRepository repo)
        {
            _repo = repo;
            _http = factory.CreateClient();
        }

        public async Task<List<Meta>> FindAllGoals(Guid id)
        {
            var usuario = await _http
                .GetAsync($"{clientUrl}/find/user/{id}");

            if(!usuario.IsSuccessStatusCode)
            {
                if(usuario.StatusCode == HttpStatusCode.NotFound) 
                    throw new Exception("Usuário não encontrado");

                throw new HttpRequestException("Não foi possível validar o usuario");
            }

            return await _repo.FindAllGoals(id);
        }

        public async Task<Meta> FindGoalById(Guid id)
        {
            return await _repo.FindGoalById(id);
        }

        public async Task SaveGoal(MetaDTO dto)
        {
            await _repo.SaveGoal(dto);
        }

        public async Task UpdateGoal(Guid id, MetaUpdate update)
        {
            await _repo.UpdateGoal(id, update);
        }

        public async Task DeleteGoal(Guid id)
        {
            await _repo.DeleteGoal(id);
        }
    }
}