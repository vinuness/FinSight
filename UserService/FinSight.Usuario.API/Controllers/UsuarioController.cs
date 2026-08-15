using FinSight.Usuario.Domain.Entities;
using FinSight.Usuario.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinSight.Usuario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpPost("register/user")]
        public async Task<ActionResult> Register([FromBody] RegisterModel register)
        {
            await _service.Register(register);
            return Ok("Registro efetuado com sucesso");
        }

        [HttpPost("log/in/user")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginModel login)
        {

            var response = await _service.Login(login);

            if (response == null) return Unauthorized();

            return Ok(response);

        }


        [HttpGet("find/user/{email}")]
        [Authorize(Roles = "Usuario,Admin")]
        public async Task<ActionResult<UsuarioModel>> FindByEmail(string email)
        {
            
            var usuario = await _service.FindByEmail(email);
            return Ok(usuario); 
        }
        

        [HttpPut("set/email/{emailAtual}")]
        [Authorize(Roles = "Usuario,Admin")]
        public async Task<ActionResult> SetEmail(string emailAtual, string emailAtualizado)
        {
            await _service.SetEmail(emailAtual, emailAtualizado);
            return Ok("Email alterado com sucesso");
        }

        [HttpPut("set/name/{email}")]
        [Authorize(Roles = "Usuario,Admin")]
        public async Task<ActionResult> SetName(string email, string name)
        {
            await _service.SetName(email, name);
            return Ok("Nome alterado com sucesso");
        }

        [HttpPut("set/pass/{email}")]
        [Authorize(Roles = "Usuario,Admin")]
        public async Task<ActionResult> SetPassword(string email, string password, string confirmPas)
        {
            await _service.SetPassword(email, password, confirmPas);
            return Ok("Senha alterada com sucesso");
        }

        [HttpDelete("delete/user/{email}")]
        [Authorize(Roles = "Usuario,Admin")]
        public async Task<ActionResult> DeleteUser(string email)
        {
            await _service.DeleteUser(email);
            return Ok($"Email {email} deletado");
        }
    }
}
