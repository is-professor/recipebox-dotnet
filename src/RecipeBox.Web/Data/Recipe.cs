namespace RecipeBox.Web.Data;

public class Recipe
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int Servings { get; set; }
    public int PrepMinutes { get; set; }

    // One step per line. A separate Step entity would be cleaner, but this keeps week 2 small.
    public string Steps { get; set; } = "";
    public bool IsPublic { get; set; } = true;
    public DateTime CreatedAt { get; set; }

    public List<RecipeIngredient> Ingredients { get; set; } = [];
}
