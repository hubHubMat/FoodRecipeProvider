using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRecipeProvider.Migrations
{
    /// <inheritdoc />
    public partial class UserDietLabels1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "UserDietLabels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DietLabelName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDietLabels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDietLabels");

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
    }
}
