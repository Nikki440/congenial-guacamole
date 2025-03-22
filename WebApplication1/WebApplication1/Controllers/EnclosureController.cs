using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class EnclosureController : Controller
    {
        private readonly DBContextDatabase _context;

        public EnclosureController(DBContextDatabase context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var enclosures = await _context.Enclosures.ToListAsync();
            return View(enclosures);
        }
    }
}
