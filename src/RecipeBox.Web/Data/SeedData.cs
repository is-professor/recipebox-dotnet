using Microsoft.EntityFrameworkCore;

namespace RecipeBox.Web.Data;

// Seed data is part of the migrations (HasData), so every fresh database gets the same rows.
// Values must be constant: no DateTime.Now or Guid.NewGuid(), or every migration would change them.
public static class SeedData
{
    private static readonly DateTime Created = new(2026, 10, 1, 12, 0, 0, DateTimeKind.Utc);

    public static void Apply(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>().HasData(
            new Recipe
            {
                Id = 1, Title = "Spinach and feta omelette", Servings = 2, PrepMinutes = 15,
                Description = "A quick, filling breakfast.",
                Steps = "Whisk the eggs.\nWilt the spinach in a pan.\nAdd the eggs and crumbled feta.\nFold and serve.",
                CreatedAt = Created
            },
            new Recipe
            {
                Id = 2, Title = "Tomato soup", Servings = 4, PrepMinutes = 40,
                Description = "Classic soup from canned tomatoes.",
                Steps = "Soften the onion in olive oil.\nAdd tomatoes and stock, simmer 20 minutes.\nBlend until smooth.",
                CreatedAt = Created
            },
            new Recipe
            {
                Id = 3, Title = "Pancakes", Servings = 4, PrepMinutes = 25,
                Description = "Thin Slovenian palačinke.",
                Steps = "Mix flour, eggs and milk into a smooth batter.\nRest 10 minutes.\nFry thin pancakes in a hot pan.",
                CreatedAt = Created
            });

        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Id = 1, Name = "egg", DefaultUnit = "pcs" },
            new Ingredient { Id = 2, Name = "spinach", DefaultUnit = "g" },
            new Ingredient { Id = 3, Name = "feta", DefaultUnit = "g" },
            new Ingredient { Id = 4, Name = "canned tomatoes", DefaultUnit = "g" },
            new Ingredient { Id = 5, Name = "onion", DefaultUnit = "pcs" },
            new Ingredient { Id = 6, Name = "vegetable stock", DefaultUnit = "ml" },
            new Ingredient { Id = 7, Name = "flour", DefaultUnit = "g" },
            new Ingredient { Id = 8, Name = "milk", DefaultUnit = "ml" });

        modelBuilder.Entity<RecipeIngredient>().HasData(
            new RecipeIngredient { RecipeId = 1, IngredientId = 1, Quantity = 4, Unit = "pcs" },
            new RecipeIngredient { RecipeId = 1, IngredientId = 2, Quantity = 100, Unit = "g" },
            new RecipeIngredient { RecipeId = 1, IngredientId = 3, Quantity = 80, Unit = "g" },
            new RecipeIngredient { RecipeId = 2, IngredientId = 4, Quantity = 800, Unit = "g" },
            new RecipeIngredient { RecipeId = 2, IngredientId = 5, Quantity = 1, Unit = "pcs" },
            new RecipeIngredient { RecipeId = 2, IngredientId = 6, Quantity = 500, Unit = "ml" },
            new RecipeIngredient { RecipeId = 3, IngredientId = 7, Quantity = 200, Unit = "g" },
            new RecipeIngredient { RecipeId = 3, IngredientId = 1, Quantity = 2, Unit = "pcs" },
            new RecipeIngredient { RecipeId = 3, IngredientId = 8, Quantity = 500, Unit = "ml" });
    }
}
