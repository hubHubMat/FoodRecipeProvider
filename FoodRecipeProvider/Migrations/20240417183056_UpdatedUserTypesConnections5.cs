using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRecipeProvider.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserTypesConnections5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCuisineTypes",
                columns: table => new
                {
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    CuisineTypeId = table.Column<int>(type: "int", nullable: false),
                    AppUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCuisineTypes", x => new { x.AppUserId, x.CuisineTypeId });
                    table.ForeignKey(
                        name: "FK_UserCuisineTypes_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCuisineTypes_CuisineTypes_CuisineTypeId",
                        column: x => x.CuisineTypeId,
                        principalTable: "CuisineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDietLabels",
                columns: table => new
                {
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    DietLabelId = table.Column<int>(type: "int", nullable: false),
                    AppUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDietLabels", x => new { x.AppUserId, x.DietLabelId });
                    table.ForeignKey(
                        name: "FK_UserDietLabels_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDietLabels_DietLabels_DietLabelId",
                        column: x => x.DietLabelId,
                        principalTable: "DietLabels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserHealthLabels",
                columns: table => new
                {
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    HealthLabelId = table.Column<int>(type: "int", nullable: false),
                    AppUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHealthLabels", x => new { x.AppUserId, x.HealthLabelId });
                    table.ForeignKey(
                        name: "FK_UserHealthLabels_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserHealthLabels_HealthLabels_HealthLabelId",
                        column: x => x.HealthLabelId,
                        principalTable: "HealthLabels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCuisineTypes_AppUserId1",
                table: "UserCuisineTypes",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserCuisineTypes_CuisineTypeId",
                table: "UserCuisineTypes",
                column: "CuisineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDietLabels_AppUserId1",
                table: "UserDietLabels",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserDietLabels_DietLabelId",
                table: "UserDietLabels",
                column: "DietLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHealthLabels_AppUserId1",
                table: "UserHealthLabels",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserHealthLabels_HealthLabelId",
                table: "UserHealthLabels",
                column: "HealthLabelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCuisineTypes");

            migrationBuilder.DropTable(
                name: "UserDietLabels");

            migrationBuilder.DropTable(
                name: "UserHealthLabels");
        }
    }
}
