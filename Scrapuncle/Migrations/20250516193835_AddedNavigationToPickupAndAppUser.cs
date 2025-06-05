using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scrapuncle.Migrations
{
    /// <inheritdoc />
    public partial class AddedNavigationToPickupAndAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Pickups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_UserId",
                table: "Pickups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pickups_AspNetUsers_UserId",
                table: "Pickups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pickups_AspNetUsers_UserId",
                table: "Pickups");

            migrationBuilder.DropIndex(
                name: "IX_Pickups_UserId",
                table: "Pickups");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Pickups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
