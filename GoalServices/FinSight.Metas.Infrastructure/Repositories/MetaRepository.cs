using FinSight.Metas.Domain.Entities;
using FinSight.Metas.Domain.Interfaces;
using FinSight.Metas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinSight.Metas.Infrastructure.Repositories
{
    public class MetaRepository : IMetaRepository
    {
        private readonly AppDbContext _con;

        public MetaRepository(AppDbContext con)
        {
            _con = con;
        }

        public async Task<List<Meta>> FindAllGoals(string email)
        {
            return await _con
                .Metas.Where(m => m.Email == email)
                .ToListAsync();
        }

        public Task<Meta> FindGoalById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveGoal(MetaDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGoal(MetaUpdate update)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGoal(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
