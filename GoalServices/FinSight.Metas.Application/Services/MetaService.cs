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

        public MetaService(HttpClient client, IMetaRepository repo)
        {
            _repo = repo;
            _http = client;
        }

        public async Task<List<Meta>> FindAllGoals(Guid id)
        {
            var usuario = await _http.GetAsync($"find/user/{id}");
            var response = await usuario.Content.ReadAsStringAsync();

            if (!usuario.IsSuccessStatusCode)
            {
                if (usuario.StatusCode == HttpStatusCode.NotFound)
                    throw new Exception("Usuário não encontrado");

                throw new HttpRequestException(
                    $"Não foi possível validar o usuario. Status: {usuario.StatusCode}");
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