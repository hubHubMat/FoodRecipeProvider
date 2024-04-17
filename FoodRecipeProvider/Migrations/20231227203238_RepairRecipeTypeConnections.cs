using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRecipeProvider.Migrations
{
    /// <inheritdoc />
    public partial class RepairRecipeTypeConnections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecipeMealTypes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecipeHealthLabels");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecipeDishTypes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecipeDietLabels");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecipeCuisineTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RecipeMealTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RecipeHealthLabels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RecipeDishTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RecipeDietLabels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RecipeCuisineTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
