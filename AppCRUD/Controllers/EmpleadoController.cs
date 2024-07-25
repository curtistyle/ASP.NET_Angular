using Microsoft.AspNetCore.Mvc;
using AppCRUD.Data;
using AppCRUD.Models;
using Microsoft.EntityFrameworkCore;


namespace AppCRUD.Controllers
{
	public class EmpleadoController : Controller
	{
		public readonly AppDBContext _context;

		public EmpleadoController(AppDBContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> Lista()
		{
			List<Empleado> lista = await _context.Empleados.ToListAsync();

			return View(lista);
		}

		[HttpGet]
		public IActionResult Nuevo()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Nuevo(Empleado empleado)
		{
			await _context.Empleados.AddAsync(empleado);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Lista));
		}


		[HttpGet]
		public async Task<IActionResult> Editar(int id)
		{
			Empleado empleado = await _context.Empleados.FirstAsync(value => value.IdEmpleado == id);
			return View(empleado);
		}

		[HttpPost]
		public async Task<IActionResult> Editar(Empleado empleado)
		{
			_context.Empleados.Update(empleado);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Lista));
		}

		[HttpGet]
		public async Task<IActionResult> Eliminar(int id)
		{
			Empleado empleado = await _context.Empleados.FirstAsync(value => value.IdEmpleado == id);

			_context.Empleados.Remove(empleado);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Lista));
		}
	}
}
