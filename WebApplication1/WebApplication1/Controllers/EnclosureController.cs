using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

namespace WebApplication1.Controllers
{
    public class EnclosureController : Controller
    {
        private readonly DBContextDatabase _context;

        public EnclosureController(DBContextDatabase context)
        {
            _context = context;
        }

        // GET: Enclosure (With Search Functionality)
        [HttpGet]
        public async Task<IActionResult> Index(string? enclosureSearch)
        {
            var enclosures = _context.Enclosures
                .Include(e => e.Animals) // Laad de dieren mee
                .AsQueryable();

            if (!string.IsNullOrEmpty(enclosureSearch))
            {
                enclosures = enclosures.Where(e =>
                    e.Name.Contains(enclosureSearch) ||
                    e.Size.ToString().Contains(enclosureSearch) ||
                    e.Climate.ToString().Contains(enclosureSearch) ||
                    e.HabitatType.ToString().Contains(enclosureSearch) ||
                    e.SecurityLevel.ToString().Contains(enclosureSearch)
                );
            }

            var enclosureList = await enclosures
                .Select(e => new Enclosure
                {
                    Id = e.Id,
                    Name = e.Name,
                    Climate = e.Climate,
                    HabitatType = e.HabitatType,
                    SecurityLevel = e.SecurityLevel,
                    Size = e.Size, // Maximale grootte van de omheining
                    SpaceLeft = e.Size - e.Animals.Sum(a => a.SpaceRequirement) // Bereken de overgebleven ruimte
                })
                .ToListAsync();

            ViewData["Enclosures"] = new SelectList(enclosureList, "Id", "Name");

            return View(enclosureList);
        }

        // GET: Enclosure/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enclosure/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Climate, HabitatType, SecurityLevel, Size")] Enclosure enclosure)
        {
            if (ModelState.IsValid)
            {
                _context.Enclosures.Add(enclosure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enclosure);
        }

        // GET: Enclosure/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enclosure = await _context.Enclosures.FindAsync(id);
            if (enclosure == null)
            {
                return NotFound();
            }

            return View(enclosure);
        }

        // POST: Enclosure/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Climate, HabitatType, SecurityLevel, Size")] Enclosure enclosure)
        {
            if (id != enclosure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enclosure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Enclosures.Any(e => e.Id == enclosure.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(enclosure);
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

        [HttpGet]
        public IActionResult AnimalsInEnclosure(int enclosureId)
        {
            List<Animal> animals = _context.Animals
              .Where(a => a.EnclosureId == enclosureId)
              .ToList();
              
            return View(animals);
        }

    }
}
