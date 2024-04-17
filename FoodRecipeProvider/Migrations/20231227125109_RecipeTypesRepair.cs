using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRecipeProvider.Migrations
{
    /// <inheritdoc />
    public partial class RecipeTypesRepair : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCuisineTypes_Recipes_RecipeId",
                table: "RecipeCuisineTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeMealTypes",
                table: "RecipeMealTypes");

            migrationBuilder.DropIndex(
                name: "IX_RecipeMealTypes_AppRecipeId",
                table: "RecipeMealTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeHealthLabels",
                table: "RecipeHealthLabels");

            migrationBuilder.DropIndex(
                name: "IX_RecipeHealthLabels_AppRecipeId",
                table: "RecipeHealthLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeDishTypes",
                table: "RecipeDishTypes");

            migrationBuilder.DropIndex(
                name: "IX_RecipeDishTypes_AppRecipeId",
                table: "RecipeDishTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeDietLabels",
                table: "RecipeDietLabels");

            migrationBuilder.DropIndex(
                name: "IX_RecipeDietLabels_AppRecipeId",
                table: "RecipeDietLabels");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "RecipeMealTypes");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "RecipeHealthLabels");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "RecipeDishTypes");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "RecipeDietLabels");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "RecipeCuisineTypes",
                newName: "AppRecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeMealTypes",
                table: "RecipeMealTypes",
                columns: new[] { "AppRecipeId", "MealTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeHealthLabels",
                table: "RecipeHealthLabels",
                columns: new[] { "AppRecipeId", "HealthLabelId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeDishTypes",
                table: "RecipeDishTypes",
                columns: new[] { "AppRecipeId", "DishTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeDietLabels",
                table: "RecipeDietLabels",
                columns: new[] { "AppRecipeId", "DietLabelId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCuisineTypes_Recipes_AppRecipeId",
                table: "RecipeCuisineTypes",
                column: "AppRecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCuisineTypes_Recipes_AppRecipeId",
                table: "RecipeCuisineTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeMealTypes",
                table: "RecipeMealTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeHealthLabels",
                table: "RecipeHealthLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeDishTypes",
                table: "RecipeDishTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeDietLabels",
                table: "RecipeDietLabels");

            migrationBuilder.RenameColumn(
                name: "AppRecipeId",
                table: "RecipeCuisineTypes",
                newName: "RecipeId");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "RecipeMealTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "RecipeHealthLabels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "RecipeDishTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "RecipeDietLabels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeMealTypes",
                table: "RecipeMealTypes",
                columns: new[] { "RecipeId", "MealTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeHealthLabels",
                table: "RecipeHealthLabels",
                columns: new[] { "RecipeId", "HealthLabelId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeDishTypes",
                table: "RecipeDishTypes",
                columns: new[] { "RecipeId", "DishTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeDietLabels",
                table: "RecipeDietLabels",
                columns: new[] { "RecipeId", "DietLabelId" });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeMealTypes_AppRecipeId",
                table: "RecipeMealTypes",
                column: "AppRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeHealthLabels_AppRecipeId",
                table: "RecipeHealthLabels",
                column: "AppRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDishTypes_AppRecipeId",
                table: "RecipeDishTypes",
                column: "AppRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDietLabels_AppRecipeId",
                table: "RecipeDietLabels",
                column: "AppRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCuisineTypes_Recipes_RecipeId",
                table: "RecipeCuisineTypes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
