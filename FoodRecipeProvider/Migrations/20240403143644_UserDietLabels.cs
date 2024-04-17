using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRecipeProvider.Migrations
{
    /// <inheritdoc />
    public partial class UserDietLabels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserHealthLabels",
                table: "UserHealthLabels");

            migrationBuilder.RenameTable(
                name: "UserHealthLabels",
                newName: "UserHealthLabel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserHealthLabel",
                table: "UserHealthLabel",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserHealthLabel",
                table: "UserHealthLabel");

            migrationBuilder.RenameTable(
                name: "UserHealthLabel",
                newName: "UserHealthLabels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserHealthLabels",
                table: "UserHealthLabels",
                column: "Id");
        }
    }
}
