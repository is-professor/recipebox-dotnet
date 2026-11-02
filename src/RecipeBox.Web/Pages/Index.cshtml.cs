using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeBox.Web.Services;

namespace RecipeBox.Web.Pages;

public class IndexModel(RecipeService recipes) : PageModel
{
    public List<RecipeSummary> Recipes { get; private set; } = [];

    public async Task OnGetAsync() => Recipes = await recipes.ListPublicAsync();
}
