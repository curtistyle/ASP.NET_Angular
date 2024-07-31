using Microsoft.AspNetCore.Mvc;
using AppLogin.Models.ViewModels;
using AppLogin.Data;
using AppLogin.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AppLogin.Controllers
{
	public class AccesoController : Controller
	{
		private readonly AppDBContext _dbContext;

        public AccesoController(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

		[HttpGet]
        public IActionResult Registrarse()
		{
			return View();
		}
		
		[HttpPost]
        public async Task<IActionResult> Registrarse(UsuarioViewModel usuario)
		{
			if (usuario.Clave != usuario.ConfirmarClave)
			{
				ViewData["Mensaje"] = "Las contraseña no coinciden";
				return View();
			}

			Usuario nuevoUsuario = new Usuario()
			{
				NombreCompleto = usuario.NombreCompleto,
				Correo = usuario.Correo,
				Clave = usuario.Clave,
			};
			await _dbContext.Usuarios.AddAsync(nuevoUsuario);
			await _dbContext.SaveChangesAsync();

			if (nuevoUsuario.IdUsuario != 0)
			{
				return RedirectToAction("Login", "Acceso");
			}

			ViewData["Mensaje"] = "No se pudo crear el usuario";
			return View();
		}

		[HttpGet]
		public IActionResult Login()
		{
			if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel usuario)
		{
			Usuario? usuarioEncontrado = await _dbContext.Usuarios
				.Where(usr => usr.Correo == usuario.Correo && usr.Clave == usuario.Clave)
				.FirstOrDefaultAsync();

			if (usuarioEncontrado == null)
			{
				ViewData["Mensaje"] = "No se encuentra el usuario";
				return View();
			}
			List<Claim> claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, usuarioEncontrado.NombreCompleto)
			};

			ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			AuthenticationProperties properties = new AuthenticationProperties()
			{
				AllowRefresh = true,
			};

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				properties
				);

			return RedirectToAction("Index", "Home");
		}
	}
}
