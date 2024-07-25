using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using WebApplicationGlosario.Controllers.Methods;
using WebApplicationGlosario.Models.ViewModels;
using WebApplicationGlosario.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplicationGlosario.Controllers
{
    public class GlosarioController : Controller
    {
        private readonly GlosarioContext _context;

        public GlosarioController(GlosarioContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //ViewData["Words"] = new SelectList(_context.Ingles, "idIngles", "palabra");
             List<Ingles> words = await
                _context.Ingles
                .Include(value => value.Espanols)
                .ToListAsync();

            return View(new GlosarioViewModel { inglesTable = words });
        }

        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GlosarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var conjuntoPalabrasEspanol = model.palabrasEspanol
                    .Split(',')
                    .Divide()
                    .Select(value =>  new Espanol() { Palabra = value})
                    .ToList();

                _context.Ingles.Add(new Ingles
                {
                    Palabra = model.palabraIngles,
                    CategoriaGramatical = model.categoriaGramatical,
                    Espanols = conjuntoPalabrasEspanol
                });

                await _context.SaveChangesAsync();                
            }
            else
            {
                return Error(ModelState);
			}
			return RedirectToAction(nameof(Index));
		}


        public IActionResult Error(ModelStateDictionary model)
        {
            var errs = model
                .SelectMany(value => value.Value.Errors)
                .Select(err => err.ErrorMessage)
                .ToList();
            return View("Error",errs);
        }

        [Route("Modificar/{id:int}")]
        public async Task<IActionResult> Modificar(int id)
        {
            Console.WriteLine("Modificar");
            Console.WriteLine($"Modificar {id}");

            var ingles = await _context.Ingles
                .Where(value => value.IdIngles == id)
                .Include(value => value.Espanols)
                .FirstAsync();

            TempData["id"] = id;
            
            return View(new InglesViewModel()
            {
                Id = ingles.IdIngles,
                Ingles = ingles.Palabra,
                CategoriaGramatical = ingles.CategoriaGramatical,
                Espanol = ingles.Espanols.Select(value => value.Palabra).Aggregate((anterior, siguiente) => anterior +", "+ siguiente)

            });
		}

        
/*		[HttpPost]
		public async Task<IActionResult> Actualizar(InglesViewModel model)
        {

            var ingles = await _context.Ingles
                .Where(value => value.IdIngles == model.Id)
                .Include(value => value.Espanols)
                .FirstAsync();

            _context.Espanols.RemoveRange(ingles.Espanols);
            _context.Update(new Ingles()
            {
                IdIngles = model.Id,
                CategoriaGramatical = model.CategoriaGramatical,
                Palabra = model.Ingles,
                Espanols = model.Espanol.Split(',')
					.Divide()
					.Select(value => new Espanol() { Palabra = value })
					.ToList()
		});

            await _context.SaveChangesAsync();

			return RedirectToAction("Index");
        }*/
		

		[HttpPost]
		public async Task<IActionResult> Actualizar(InglesViewModel model)
		{

			var ingles = await _context.Ingles
				.Where(value => value.IdIngles == model.Id)
				.Include(value => value.Espanols)
				.FirstAsync();

			_context.Espanols.RemoveRange(ingles.Espanols);
			_context.Update(new Ingles()
			{
				IdIngles = model.Id,
				CategoriaGramatical = model.CategoriaGramatical,
				Palabra = model.Ingles,
				Espanols = model.Espanol.Split(',')
					.Divide()
					.Select(value => new Espanol() { Palabra = value })
					.ToList()
			});

			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}

	}
}
