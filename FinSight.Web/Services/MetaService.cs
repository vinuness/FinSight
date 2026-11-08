using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSight.Web.Interfaces;
using FinSight.Web.Models.Metas;

namespace FinSight.Web.Services
{
    public class MetaService : IMetaService
    {
        private readonly HttpClient _http;

        public MetaService(HttpClient http)
        {
            _http = http;
        }

        public async Task DeleteGoal(Guid id)
        {
            await _http.DeleteAsync($"delete/goal/{id}");
        }

        public async Task<List<Meta>?> FindAllGoals(Guid id)
        {
            return await _http.GetFromJsonAsync<List<Meta>>($"find/all/goals/{id}");
        }

        public async Task<Meta?> FindGoalById(Guid id)
        {
            return await _http.GetFromJsonAsync<Meta>($"find/goal/{id}");
        }

        public async Task SaveGoal(MetaDTO dto)
        {
            await _http.PostAsJsonAsync($"save/goal", dto);
        }

        public async Task UpdateGoal(Guid id, MetaUpdate update)
        {
            await _http.PutAsJsonAsync($"update/goal/{id}", update);
        }
    }
}