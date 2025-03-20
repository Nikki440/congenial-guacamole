using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;  // Zorg ervoor dat de namespace klopt!

var builder = WebApplication.CreateBuilder(args);

// DBContextDatabase registreren met dependency injection
builder.Services.AddDbContext<DBContextDatabase>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DBContextDatabase>();

    // Apply any pending migrations
    DBContextDatabase.Migrate();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();