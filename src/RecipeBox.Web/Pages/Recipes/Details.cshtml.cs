using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeBox.Web.Services;

namespace RecipeBox.Web.Pages.Recipes;

public class DetailsModel(RecipeService recipes) : PageModel
{
    public RecipeDetails Recipe { get; private set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var recipe = await recipes.GetPublicAsync(id);
        if (recipe is null) return NotFound();

        Recipe = recipe;
        return Page();
    }
}
