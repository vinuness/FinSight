using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinSight.Metas.Domain.Interfaces;
using FinSight.Metas.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace FinSight.Metas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetaController : ControllerBase
    {
        private readonly IMetaService _service;

        public MetaController(IMetaService service)
        {
            _service = service;
        }

        [HttpGet("find/all/goals/{id}")]
        [Authorize]
        public async Task<ActionResult<List<Meta>>> FindAllGoals(Guid id)
        {
            List<Meta> metas = await _service.FindAllGoals(id);
            return Ok(metas);
        }

        [HttpPost("save/goal")]
        [Authorize]
        public async Task<ActionResult> SaveGoal([FromBody] MetaDTO dto)
        {
            await _service.SaveGoal(dto);
            return Ok("Meta cadastrada com sucesso");
        }

        [HttpGet("find/goal/{id}")]
        [Authorize]
        public async Task<ActionResult<Meta>> FindGoalById(Guid id)
        {
            Meta meta = await _service.FindGoalById(id);
            return Ok(meta);
        }

        [HttpPut("update/goal/{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateGoal(Guid id, [FromBody] MetaUpdate update)
        {
            await _service.UpdateGoal(id, update);
            return Ok("Meta atualizada com sucesso");
        }

        [HttpDelete("delete/goal/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteGoal(Guid id)
        {
            await _service.DeleteGoal(id);
            return Ok("Meta deletada com sucesso");
        }
    }
}