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

    // GET: Animal (With Search Functionality)
    [HttpGet]
    public async Task<IActionResult> Index(string? searchString, int? categoryId)
    {
        var animals = _context.Animals
            .Include(a => a.Category)
            .Include(a => a.Enclosure)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            animals = animals.Where(a =>
                a.Name.Contains(searchString) ||
                a.Species.Contains(searchString) ||
                a.Category.Name.Contains(searchString) ||
                a.Enclosure.Name.Contains(searchString)
            );
        }
        // Apply category filter
        if (categoryId.HasValue)
        {
            animals = animals.Where(a => a.CategoryId == categoryId.Value);
        }

        // Prepare filter data for dropdowns
        ViewData["Categories"] = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        ViewData["Enclosures"] = new SelectList(await _context.Enclosures.ToListAsync(), "Id", "Name");

        return View(await animals.ToListAsync());
    }


    // GET: Animal/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
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
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", animal.CategoryId);
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
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", animal.CategoryId);
        ViewData["EnclosureId"] = new SelectList(_context.Enclosures, "Id", "Name", animal.EnclosureId); // Add this line
        return View(animal);
    }

    // POST: Animal/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Species,CategoryId,Size,DietaryClass,ActivityPattern,Prey,SpaceRequirement,SecurityRequirement,EnclosureId")] Animal animal)
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
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", animal.CategoryId);
        ViewData["EnclosureId"] = new SelectList(_context.Enclosures, "Id", "Name", animal.EnclosureId); // Add this line
        return View(animal);
    }
    // GET: Animal/Delete/5
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var animal = await _context.Animals.FindAsync(id);
        if (animal == null)
        {
            return NotFound();
        }

        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    private bool AnimalExists(int id)
    {
        return _context.Animals.Any(e => e.Id == id);
    }
}


