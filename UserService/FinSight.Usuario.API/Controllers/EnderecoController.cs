using FinSight.Usuario.Domain.Entities;
using FinSight.Usuario.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FinSight.Usuario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _service;

        public EnderecoController(IEnderecoService service)
        {
            _service = service;
        }

        [HttpPost("add/adress/{email}")]
        public async Task<ActionResult> AddAdress(string email, [FromBody] EnderecoDTO endereco)
        {
            await _service.AddAdress(email, endereco);
            return Ok("email adicionado com sucesso");
        }

        [HttpDelete("delete/{email}/adress/{id}")]
        public async Task<ActionResult> RemoveAdress(string email, int id)
        {
            await _service.RemoveAdress(email, id);
            return Ok("email removido com sucesso");
        }

        [HttpPut("set/{email}/adress/{id}")]
        public async Task<ActionResult> SetAdress(string email, int id, [FromBody] EnderecoDTO dto)
        {
            await _service.SetAdress(email, id, dto);
            return Ok("email alterado com sucesso");
        }
    }
}
