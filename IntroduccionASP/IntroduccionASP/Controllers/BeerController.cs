﻿using IntroduccionASP.Models;
using IntroduccionASP.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IntroduccionASP.Controllers
{
	public class BeerController : Controller
	{
		private readonly PubContext _context;

		public BeerController(PubContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var beers = _context.Beers.Include(b => b.Brand);
			return View(await beers.ToListAsync());
		}

		public IActionResult Create()
		{
			ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeerViewModel model)
        {
			if (ModelState.IsValid)
			{
				var beer = new Beer()
				{
					Name = model.Name,
					BrandId = model.BrandId,
				};
				_context.Add(beer);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name", model.BrandId);
            return View(model);
        }
    }
}
