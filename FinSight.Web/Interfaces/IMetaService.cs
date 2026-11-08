using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSight.Web.Models.Metas;

namespace FinSight.Web.Interfaces
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