using FinSight.Metas.Domain.Entities;

namespace FinSight.Metas.Domain.Interfaces
{
    public interface IMetaRepository
    {
        Task<List<Meta>> FindAllGoals(string email);
        Task SaveGoal(MetaDTO dto);
        Task<Meta> FindGoalById(Guid id);
        Task UpdateGoal(MetaUpdate update);
        Task DeleteGoal(Guid id);
    }
}
