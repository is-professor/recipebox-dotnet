namespace RecipeBox.Web.Data;

// Join entity of the M:N relationship Recipe ↔ Ingredient. It is a real entity (not a hidden
// join table) because the relationship carries data: quantity and unit.
public class RecipeIngredient
{
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;

    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;

    public decimal Quantity { get; set; }
    public string? Unit { get; set; }
}
