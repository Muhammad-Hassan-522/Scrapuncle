using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scrapuncle.Migrations
{
    /// <inheritdoc />
    public partial class AddedScrapItemsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DealerId",
                table: "Pickups",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ScrapItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_DealerId",
                table: "Pickups",
                column: "DealerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pickups_AspNetUsers_DealerId",
                table: "Pickups",
                column: "DealerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pickups_AspNetUsers_DealerId",
                table: "Pickups");

            migrationBuilder.DropTable(
                name: "ScrapItems");

            migrationBuilder.DropIndex(
                name: "IX_Pickups_DealerId",
                table: "Pickups");

            migrationBuilder.AlterColumn<string>(
                name: "DealerId",
                table: "Pickups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
