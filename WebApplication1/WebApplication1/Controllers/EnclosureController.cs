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

        // POST: Enclosure/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var enclosure = await _context.Enclosures.Include(e => e.Animals).FirstOrDefaultAsync(e => e.Id == id);
            if (enclosure == null)
            {
                return NotFound();
            }

            // Set EnclosureId of associated animals to null
            foreach (var animal in enclosure.Animals)
            {
                animal.Enclosure = null;
            }

            _context.Enclosures.Remove(enclosure);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
