using FinSight.Metas.Domain.Entities;

namespace FinSight.Metas.Domain.Interfaces
{
    public interface IMetaRepository
    {
        Task<List<Meta>> FindAllGoals(Guid id);
        Task SaveGoal(MetaDTO dto);
        Task<Meta> FindGoalById(Guid id);
        Task UpdateGoal(Guid id, MetaUpdate update);
        Task DeleteGoal(Guid id);
    }
}
