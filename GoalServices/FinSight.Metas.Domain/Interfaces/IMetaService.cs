using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSight.Metas.Domain.Entities;

namespace FinSight.Metas.Domain.Interfaces
{
    public interface IMetaService
    {
        Task<List<Meta>> FindAllGoals(Guid id);
        Task SaveGoal(MetaDTO dto);
        Task<Meta> FindGoalById(Guid id);
        Task UpdateGoal(Guid id, MetaUpdate update);
        Task DeleteGoal(Guid id);
    }
}