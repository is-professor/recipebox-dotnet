namespace RecipeBox.Web.Data;

public class Ingredient
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? DefaultUnit { get; set; }

    public List<RecipeIngredient> Recipes { get; set; } = [];
}
