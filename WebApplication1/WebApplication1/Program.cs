using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;  // Correct namespace for your DBContextDatabase

var builder = WebApplication.CreateBuilder(args);

// Register the DBContextDatabase DbContext
builder.Services.AddDbContext<DBContextDatabase>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DBContextDatabase>();  // Correct reference

    // Apply any pending migrations
    context.Database.Migrate();  // Apply migrations
}

app.Run();