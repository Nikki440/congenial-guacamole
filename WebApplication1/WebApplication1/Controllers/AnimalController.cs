using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Bogus;

public class AnimalController : Controller
{
    private readonly DBContextDatabase _context;

    public AnimalController(DBContextDatabase context)
    {
        _context = context;
    }

    // AutoAssign
    [HttpPost]
    public async Task<IActionResult> AutoAssign(bool resetEnclosures)
    {
        var animals = await _context.Animals.ToListAsync();

        if (resetEnclosures)
        {
            // Verwijder alle verblijven en huidige indeling
            _context.Enclosures.RemoveRange(_context.Enclosures);
            await _context.SaveChangesAsync();
        }

        var enclosures = await _context.Enclosures.ToListAsync();

        foreach (var animal in animals.Where(a => a.EnclosureId == null || a.EnclosureId == 0))
        {
            var assignedEnclosure = enclosures
                .FirstOrDefault(e =>
                    //e.SpaceLeft() >= animal.SpaceRequirement &&
                    e.SecurityLevel >= animal.SecurityRequirement);

            if (assignedEnclosure == null)
            {
                // Genereer willekeurig verblijf met Bogus
                var faker = new Faker<Enclosure>()
                    .RuleFor(e => e.Name, f => f.Lorem.Word()) // Willekeurige naam
                    .RuleFor(e => e.Size, f => f.Random.Int(400, 1000)) // Willekeurige grootte tussen 400 en 1000
                    .RuleFor(e => e.SecurityLevel, f => f.PickRandom<SecurityLevelEnum>()) // Willekeurig beveiligingsniveau
                    .RuleFor(e => e.Climate, f => f.PickRandom<ClimateEnum>()) // Willekeurig klimaat
                    .RuleFor(e => e.HabitatType, f => f.PickRandom< flagsEnum >()); // Correcte enum gebruiken

                assignedEnclosure = faker.Generate();

                _context.Enclosures.Add(assignedEnclosure);
                await _context.SaveChangesAsync();
                enclosures.Add(assignedEnclosure);
            }

            // Koppel dier aan verblijf
            animal.EnclosureId = assignedEnclosure.Id;
            _context.Update(animal);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    // GET: Animal (With Search Functionality and filter)
    [HttpGet]
    public async Task<IActionResult> Index(string? searchString, int? categoryId, string timeOfDay)
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


        // Filter animals based TOD
        if (timeOfDay == "sunset")
        {
            animals = animals.Where(a => a.ActivityPattern == ActivityPatternEnum.Nocturnal || a.ActivityPattern == ActivityPatternEnum.Cathemeral);
        }
        else if (timeOfDay == "sunrise")
        {
            animals = animals.Where(a => a.ActivityPattern == ActivityPatternEnum.Diurnal || a.ActivityPattern == ActivityPatternEnum.Cathemeral);
        }
        else
        {
            // No time of day selected show all animals
            animals = animals.Where(a => a.ActivityPattern == ActivityPatternEnum.Cathemeral || a.ActivityPattern == ActivityPatternEnum.Diurnal || a.ActivityPattern == ActivityPatternEnum.Nocturnal);
        }

        // Prepare filter data for dropdowns
        ViewData["Categories"] = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        ViewData["Enclosures"] = new SelectList(await _context.Enclosures.ToListAsync(), "Id", "Name");


        return View(await animals.ToListAsync());
    }
    public async Task<IActionResult> RemoveAllAnimalsFromEnclosures()
    {
        var animals = await _context.Animals.ToListAsync();

        foreach (var animal in animals)
        {
            animal.EnclosureId = null; // Or use `0` if your database doesn't allow null
            _context.Update(animal);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
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
            Random random = new Random();
            var randomHour = random.Next(0, 24); // Random hour between 0 and 23
            var randomMinute = random.Next(0, 60); // Random minute between 0 and 59

            // Assign a random time within a specific date (e.g., today)
            animal.FeedingTime = DateTime.Today.AddHours(randomHour).AddMinutes(randomMinute).TimeOfDay;


            // Add the animal to the database
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
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Species,CategoryId,Size,DietaryClass,ActivityPattern,Prey,SpaceRequirement,SecurityRequirement,EnclosureId,FeedingTime")] Animal animal)
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
        ViewData["EnclosureId"] = new SelectList(_context.Enclosures, "Id", "Name", animal.EnclosureId);
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


