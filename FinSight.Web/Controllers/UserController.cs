using System.Security.Claims;
using FinSight.Web.Interfaces;
using FinSight.Web.Models.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace FinSight.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _user;

        public UserController(IUserService user)
        {
            _user = user;
        }

        public IActionResult Login()
        {
            ViewData["HideSide"] = true;
            return View();
        }

        [HttpPost]  
        public async Task<IActionResult> Login(LoginModel login)
        {
            var res = await _user.Login(login);

            if(res == null)
            {
                TempData["Error"] = "Dados incorretos ou usuario inexistente";
                return RedirectToAction("Login");
            }

            Response.Cookies.Append("jwt", res.Token, new CookieOptions
            {
               HttpOnly = true,
               Secure = true,
               SameSite = SameSiteMode.Strict,
               Expires = DateTimeOffset.UtcNow.AddDays(1) 
            });

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Cadastrar()
        {
            ViewData["HideSide"] = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(RegisterModel register)
        {
            await _user.Cadastrar(register);
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Perfil()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var jwt = Request.Cookies["jwt"];

            if(jwt == null || email == null) return RedirectToAction("Login");

            Usuario usuario = await _user.FindEmail(email);
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> SetEmail(string emailAtual, string emailAtualizado)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if(email.Equals(emailAtual)) {
                await _user.SetEmail(emailAtual, emailAtualizado);
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("SetEmail");
            }
        }

        public IActionResult SetEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetNome(string email, string name)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if(userEmail.Equals(email)) {
                await _user.SetNome(email, name);
                return RedirectToAction("Perfil");
            }
            else
            {
                return RedirectToAction("SetEmail");
            }
        }

        public IActionResult SetNome()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetPass(UpdatePass pass)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            await _user.SetPass(email, pass);
            return RedirectToAction("Login");
        }

        public IActionResult SetPass()
        {
            return View();
        }
    }
}