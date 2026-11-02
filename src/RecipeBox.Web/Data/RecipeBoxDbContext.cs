using Microsoft.EntityFrameworkCore;

namespace RecipeBox.Web.Data;

public class RecipeBoxDbContext(DbContextOptions<RecipeBoxDbContext> options) : DbContext(options)
{
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>(recipe =>
        {
            recipe.Property(r => r.Title).HasMaxLength(200);
            recipe.Property(r => r.Description).HasMaxLength(2000);
            recipe.ToTable(t => t.HasCheckConstraint("CK_Recipe_Servings", "Servings BETWEEN 1 AND 50"));
        });

        modelBuilder.Entity<Ingredient>(ingredient =>
        {
            ingredient.Property(i => i.Name).HasMaxLength(100);
            ingredient.HasIndex(i => i.Name).IsUnique();   // business rule 4: names are unique
        });

        modelBuilder.Entity<RecipeIngredient>(ri =>
        {
            // Composite primary key: an ingredient appears at most once per recipe.
            ri.HasKey(x => new { x.RecipeId, x.IngredientId });
            ri.HasOne(x => x.Recipe).WithMany(r => r.Ingredients).HasForeignKey(x => x.RecipeId);
            ri.HasOne(x => x.Ingredient).WithMany(i => i.Recipes).HasForeignKey(x => x.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);   // don't silently remove ingredients from recipes
            ri.Property(x => x.Unit).HasMaxLength(20);
        });

        SeedData.Apply(modelBuilder);
    }
}
