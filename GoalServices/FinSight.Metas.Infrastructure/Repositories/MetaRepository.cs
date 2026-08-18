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

        public async Task<List<Meta>> FindAllGoals(Guid id)
        {   
            return await _con
                .Metas.Where(m => m.UsuarioId == id)
                .ToListAsync();
        }

        public async Task<Meta?> FindGoalById(Guid id)
        {
            return await _con.Metas
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task SaveGoal(MetaDTO dto)
        {
            var Meta = new Meta{
                UsuarioId = dto.UsuarioId,
                Nome = dto.Nome ?? "Nova Meta",
                Descricao = dto.Descricao,
                ValorAtual = dto.ValorAtual,
                ValorDesejado = dto.ValorDesejado
            }; 

            await _con.Metas.AddAsync(Meta);
            await _con.SaveChangesAsync();
        }

        public async Task UpdateGoal(Guid id, MetaUpdate update)
        {
            var meta = await _con.Metas
                .FirstOrDefaultAsync(m => m.Id == id);

            if(meta != null)
            {
                meta.Descricao = update.Descricao ?? meta.Descricao;
                meta.Nome = update.Nome ?? meta.Nome;
                if(update.ValorAtual.HasValue) meta.ValorAtual = update.ValorAtual.Value; 
                if(update.ValorDesejado.HasValue) meta.ValorDesejado = update.ValorDesejado.Value;
            }

            await _con.SaveChangesAsync();
        }

        public async Task DeleteGoal(Guid id)
        {
            var meta = await _con.Metas
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if(meta != null)
            {
                _con.Metas.Remove(meta);
            }

            await _con.SaveChangesAsync();
        }
    }
}
