using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRecipeProvider.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserTypesConnections3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCuisineTypes_AspNetUsers_AppUserId",
                table: "UserCuisineTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDietLabels_AspNetUsers_AppUserId",
                table: "UserDietLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_UserHealthLabels_AspNetUsers_AppUserId",
                table: "UserHealthLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserHealthLabels",
                table: "UserHealthLabels");

            migrationBuilder.DropIndex(
                name: "IX_UserHealthLabels_AppUserId",
                table: "UserHealthLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDietLabels",
                table: "UserDietLabels");

            migrationBuilder.DropIndex(
                name: "IX_UserDietLabels_AppUserId",
                table: "UserDietLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCuisineTypes",
                table: "UserCuisineTypes");

            migrationBuilder.DropIndex(
                name: "IX_UserCuisineTypes_AppUserId",
                table: "UserCuisineTypes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserHealthLabels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserDietLabels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserCuisineTypes");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "UserHealthLabels",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "UserHealthLabels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "UserDietLabels",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "UserDietLabels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "UserCuisineTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "UserCuisineTypes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserHealthLabels",
                table: "UserHealthLabels",
                columns: new[] { "AppUserId", "HealthLabelId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDietLabels",
                table: "UserDietLabels",
                columns: new[] { "AppUserId", "DietLabelId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCuisineTypes",
                table: "UserCuisineTypes",
                columns: new[] { "AppUserId", "CuisineTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserHealthLabels_AppUserId1",
                table: "UserHealthLabels",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserDietLabels_AppUserId1",
                table: "UserDietLabels",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserCuisineTypes_AppUserId1",
                table: "UserCuisineTypes",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCuisineTypes_AspNetUsers_AppUserId1",
                table: "UserCuisineTypes",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDietLabels_AspNetUsers_AppUserId1",
                table: "UserDietLabels",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHealthLabels_AspNetUsers_AppUserId1",
                table: "UserHealthLabels",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCuisineTypes_AspNetUsers_AppUserId1",
                table: "UserCuisineTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDietLabels_AspNetUsers_AppUserId1",
                table: "UserDietLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_UserHealthLabels_AspNetUsers_AppUserId1",
                table: "UserHealthLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserHealthLabels",
                table: "UserHealthLabels");

            migrationBuilder.DropIndex(
                name: "IX_UserHealthLabels_AppUserId1",
                table: "UserHealthLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDietLabels",
                table: "UserDietLabels");

            migrationBuilder.DropIndex(
                name: "IX_UserDietLabels_AppUserId1",
                table: "UserDietLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCuisineTypes",
                table: "UserCuisineTypes");

            migrationBuilder.DropIndex(
                name: "IX_UserCuisineTypes_AppUserId1",
                table: "UserCuisineTypes");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "UserHealthLabels");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "UserDietLabels");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "UserCuisineTypes");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "UserHealthLabels",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserHealthLabels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "UserDietLabels",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserDietLabels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "UserCuisineTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserCuisineTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserHealthLabels",
                table: "UserHealthLabels",
                columns: new[] { "UserId", "HealthLabelId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDietLabels",
                table: "UserDietLabels",
                columns: new[] { "UserId", "DietLabelId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCuisineTypes",
                table: "UserCuisineTypes",
                columns: new[] { "UserId", "CuisineTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserHealthLabels_AppUserId",
                table: "UserHealthLabels",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDietLabels_AppUserId",
                table: "UserDietLabels",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCuisineTypes_AppUserId",
                table: "UserCuisineTypes",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCuisineTypes_AspNetUsers_AppUserId",
                table: "UserCuisineTypes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDietLabels_AspNetUsers_AppUserId",
                table: "UserDietLabels",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHealthLabels_AspNetUsers_AppUserId",
                table: "UserHealthLabels",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
