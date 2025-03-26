using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class CategoryController : Controller
{
    private readonly DBContextDatabase _context;

    public CategoryController(DBContextDatabase context)
    {
        _context = context;
    }

    // GET: Category (With Search and Enclosure Filter)
    [HttpGet]
    public async Task<IActionResult> Index(string? categorySearch, int? enclosureId)
    {
        var categories = _context.Categories
            .Include(c => c.Animals) // Include related Animals
            .ThenInclude(a => a.Enclosure) // Include Enclosure details
            .AsQueryable();

        // Apply search filter
        if (!string.IsNullOrEmpty(categorySearch))
        {
            categories = categories.Where(c => c.Name.Contains(categorySearch));
        }

        // Apply enclosure filter
        if (enclosureId.HasValue)
        {
            categories = categories.Where(c => c.Animals.Any(a => a.EnclosureId == enclosureId.Value));
        }

        // Populate dropdown list for Enclosures
        ViewData["Enclosures"] = new SelectList(await _context.Enclosures.ToListAsync(), "Id", "Name");

        return View(await categories.ToListAsync()); // Return filtered category list
    }


    // GET: Category/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Category/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name")] Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    // GET: Category/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    // POST: Category/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id))
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
        return View(category);
    }

    // POST: Category/Delete/5
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Animals) // Haal ook de dieren op
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        // Verwijder eerst alle dieren in de categorie
        _context.Animals.RemoveRange(category.Animals);

        // Daarna de categorie zelf verwijderen
        _context.Categories.Remove(category);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.Id == id);
    }


}
