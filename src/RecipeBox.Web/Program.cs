using Microsoft.EntityFrameworkCore;
using RecipeBox.Web.Data;
using RecipeBox.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RecipeBoxDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("RecipeBox")));
builder.Services.AddScoped<RecipeService>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Apply migrations on startup so a fresh clone works with just `dotnet run`.
// Fine for development; week 8 discusses why production needs something more careful.
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<RecipeBoxDbContext>().Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

// S1: the same data as JSON, for the client-side rendered page (and, from week 4, the real API).
app.MapGet("/api/recipes", (RecipeService recipes) => recipes.ListPublicAsync());

app.Run();
