using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class AnimalController : Controller
{
    private readonly DBContextDatabase _context;

    public AnimalController(DBContextDatabase context)
    {
        _context = context;
    }

    // GET: Animal
    public async Task<IActionResult> Index()
    {
        var animals = await _context.Animals.Include(a => a.Category).ToListAsync();
        return View(animals);
    }

    // GET: Animal/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name"); // Use CategoryId instead of Category
        return View();
    }

    // POST: Animal/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Species,CategoryId,Size,DietaryClass,ActivityPattern,Prey,SpaceRequirement,SecurityRequirement")] Animal animal)
    {
        if (ModelState.IsValid)
        {
            _context.Add(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", animal.CategoryId); // Ensure we use CategoryId
        return View(animal);
    }

    // GET: Animal/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var animal = await _context.Animals.FindAsync(id);
        if (animal == null)
        {
            return NotFound();
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", animal.CategoryId); // Ensure CategoryId is set
        return View(animal);
    }

    // POST: Animal/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Species,CategoryId,Size,DietaryClass,ActivityPattern,Prey,SpaceRequirement,SecurityRequirement")] Animal animal)
    {
        if (id != animal.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(animal);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(animal.Id))
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
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", animal.CategoryId); // Ensure CategoryId is set
        return View(animal);
    }

    // GET: Animal/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var animal = await _context.Animals
            .Include(a => a.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (animal == null)
        {
            return NotFound();
        }

        return View(animal);
    }

    // POST: Animal/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var animal = await _context.Animals.FindAsync(id);
        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AnimalExists(int id)
    {
        return _context.Animals.Any(e => e.Id == id);
    }
}
