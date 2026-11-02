using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeBox.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    Servings = table.Column<int>(type: "INTEGER", nullable: false),
                    PrepMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    Steps = table.Column<string>(type: "TEXT", nullable: false),
                    IsPublic = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.CheckConstraint("CK_Recipe_Servings", "Servings BETWEEN 1 AND 50");
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CreatedAt", "Description", "IsPublic", "PrepMinutes", "Servings", "Steps", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 10, 1, 12, 0, 0, 0, DateTimeKind.Utc), "A quick, filling breakfast.", true, 15, 2, "Whisk the eggs.\nWilt the spinach in a pan.\nAdd the eggs and crumbled feta.\nFold and serve.", "Spinach and feta omelette" },
                    { 2, new DateTime(2026, 10, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Classic soup from canned tomatoes.", true, 40, 4, "Soften the onion in olive oil.\nAdd tomatoes and stock, simmer 20 minutes.\nBlend until smooth.", "Tomato soup" },
                    { 3, new DateTime(2026, 10, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Thin Slovenian palačinke.", true, 25, 4, "Mix flour, eggs and milk into a smooth batter.\nRest 10 minutes.\nFry thin pancakes in a hot pan.", "Pancakes" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
