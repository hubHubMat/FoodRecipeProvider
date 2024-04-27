using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRecipeProvider.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRecipeMealAndDishTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeDishTypes");

            migrationBuilder.DropTable(
                name: "RecipeMealTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecipeDishTypes",
                columns: table => new
                {
                    AppRecipeId = table.Column<int>(type: "int", nullable: false),
                    DishTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeDishTypes", x => new { x.AppRecipeId, x.DishTypeId });
                    table.ForeignKey(
                        name: "FK_RecipeDishTypes_DishTypes_DishTypeId",
                        column: x => x.DishTypeId,
                        principalTable: "DishTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeDishTypes_Recipes_AppRecipeId",
                        column: x => x.AppRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeMealTypes",
                columns: table => new
                {
                    AppRecipeId = table.Column<int>(type: "int", nullable: false),
                    MealTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeMealTypes", x => new { x.AppRecipeId, x.MealTypeId });
                    table.ForeignKey(
                        name: "FK_RecipeMealTypes_MealTypes_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeMealTypes_Recipes_AppRecipeId",
                        column: x => x.AppRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDishTypes_DishTypeId",
                table: "RecipeDishTypes",
                column: "DishTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeMealTypes_MealTypeId",
                table: "RecipeMealTypes",
                column: "MealTypeId");
        }
    }
}
