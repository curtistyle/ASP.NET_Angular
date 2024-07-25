using IntroduccionASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntroduccionASP.Controllers
{
    public class BrandController : Controller
    {
        private readonly PubContext _context;

        //
        public BrandController(PubContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            // _context = db, Brand = table, ToList=get list of Brand object type
            return View(await _context.Brands.ToListAsync());
        }
    }
}
