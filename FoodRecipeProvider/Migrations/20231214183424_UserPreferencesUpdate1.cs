using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRecipeProvider.Migrations
{
    /// <inheritdoc />
    public partial class UserPreferencesUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreferredMealType",
                table: "UserPreferences",
                newName: "MealTypeId");

            migrationBuilder.RenameColumn(
                name: "PreferredHealthLabel",
                table: "UserPreferences",
                newName: "HealthLabelId");

            migrationBuilder.RenameColumn(
                name: "PreferredDishType",
                table: "UserPreferences",
                newName: "DishTypeId");

            migrationBuilder.RenameColumn(
                name: "PreferredDietLabel",
                table: "UserPreferences",
                newName: "DietLabelId");

            migrationBuilder.RenameColumn(
                name: "PreferredCuisineType",
                table: "UserPreferences",
                newName: "CuisineTypeId");

            migrationBuilder.CreateTable(
                name: "CuisineTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuisineTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DietLabels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietLabels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DishTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthLabels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthLabels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_CuisineTypeId",
                table: "UserPreferences",
                column: "CuisineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_DietLabelId",
                table: "UserPreferences",
                column: "DietLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_DishTypeId",
                table: "UserPreferences",
                column: "DishTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_HealthLabelId",
                table: "UserPreferences",
                column: "HealthLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_MealTypeId",
                table: "UserPreferences",
                column: "MealTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPreferences_CuisineTypes_CuisineTypeId",
                table: "UserPreferences",
                column: "CuisineTypeId",
                principalTable: "CuisineTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPreferences_DietLabels_DietLabelId",
                table: "UserPreferences",
                column: "DietLabelId",
                principalTable: "DietLabels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPreferences_DishTypes_DishTypeId",
                table: "UserPreferences",
                column: "DishTypeId",
                principalTable: "DishTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPreferences_HealthLabels_HealthLabelId",
                table: "UserPreferences",
                column: "HealthLabelId",
                principalTable: "HealthLabels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPreferences_MealTypes_MealTypeId",
                table: "UserPreferences",
                column: "MealTypeId",
                principalTable: "MealTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPreferences_CuisineTypes_CuisineTypeId",
                table: "UserPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPreferences_DietLabels_DietLabelId",
                table: "UserPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPreferences_DishTypes_DishTypeId",
                table: "UserPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPreferences_HealthLabels_HealthLabelId",
                table: "UserPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPreferences_MealTypes_MealTypeId",
                table: "UserPreferences");

            migrationBuilder.DropTable(
                name: "CuisineTypes");

            migrationBuilder.DropTable(
                name: "DietLabels");

            migrationBuilder.DropTable(
                name: "DishTypes");

            migrationBuilder.DropTable(
                name: "HealthLabels");

            migrationBuilder.DropTable(
                name: "MealTypes");

            migrationBuilder.DropIndex(
                name: "IX_UserPreferences_CuisineTypeId",
                table: "UserPreferences");

            migrationBuilder.DropIndex(
                name: "IX_UserPreferences_DietLabelId",
                table: "UserPreferences");

            migrationBuilder.DropIndex(
                name: "IX_UserPreferences_DishTypeId",
                table: "UserPreferences");

            migrationBuilder.DropIndex(
                name: "IX_UserPreferences_HealthLabelId",
                table: "UserPreferences");

            migrationBuilder.DropIndex(
                name: "IX_UserPreferences_MealTypeId",
                table: "UserPreferences");

            migrationBuilder.RenameColumn(
                name: "MealTypeId",
                table: "UserPreferences",
                newName: "PreferredMealType");

            migrationBuilder.RenameColumn(
                name: "HealthLabelId",
                table: "UserPreferences",
                newName: "PreferredHealthLabel");

            migrationBuilder.RenameColumn(
                name: "DishTypeId",
                table: "UserPreferences",
                newName: "PreferredDishType");

            migrationBuilder.RenameColumn(
                name: "DietLabelId",
                table: "UserPreferences",
                newName: "PreferredDietLabel");

            migrationBuilder.RenameColumn(
                name: "CuisineTypeId",
                table: "UserPreferences",
                newName: "PreferredCuisineType");
        }
    }
}
