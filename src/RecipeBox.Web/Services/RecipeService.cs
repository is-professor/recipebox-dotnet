using Microsoft.EntityFrameworkCore;
using RecipeBox.Web.Data;

namespace RecipeBox.Web.Services;

public record RecipeSummary(int Id, string Title, int Servings, int PrepMinutes, int IngredientCount);

public record IngredientLine(string Name, decimal Quantity, string? Unit);

public record RecipeDetails(
    int Id, string Title, string? Description, int Servings, int PrepMinutes,
    IReadOnlyList<string> Steps, IReadOnlyList<IngredientLine> Ingredients);

// Pages call the service, the service talks to the database (week 2 README, "separate the layers").
public class RecipeService(RecipeBoxDbContext db)
{
    // E2: the ingredient count is part of the projection, so EF Core translates it into a
    // subquery and the whole list is ONE SQL query (no N+1).
    public Task<List<RecipeSummary>> ListPublicAsync() =>
        db.Recipes
            .Where(r => r.IsPublic)
            .OrderBy(r => r.Title)
            .Select(r => new RecipeSummary(r.Id, r.Title, r.Servings, r.PrepMinutes, r.Ingredients.Count))
            .ToListAsync();

    public async Task<RecipeDetails?> GetPublicAsync(int id)
    {
        var recipe = await db.Recipes
            .Where(r => r.Id == id && r.IsPublic)
            .Select(r => new
            {
                r.Id, r.Title, r.Description, r.Servings, r.PrepMinutes, r.Steps,
                Ingredients = r.Ingredients
                    .OrderBy(ri => ri.Ingredient.Name)
                    .Select(ri => new IngredientLine(ri.Ingredient.Name, ri.Quantity, ri.Unit))
                    .ToList()
            })
            .SingleOrDefaultAsync();

        if (recipe is null) return null;

        var steps = recipe.Steps.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return new RecipeDetails(recipe.Id, recipe.Title, recipe.Description, recipe.Servings,
            recipe.PrepMinutes, steps, recipe.Ingredients);
    }
}
