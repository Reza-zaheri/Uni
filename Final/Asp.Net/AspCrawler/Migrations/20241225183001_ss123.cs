using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspCrawler.Migrations
{
    /// <inheritdoc />
    public partial class ss123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrolls_Products_IdP",
                table: "Enrolls");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolls_Products_IdP",
                table: "Enrolls",
                column: "IdP",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrolls_Products_IdP",
                table: "Enrolls");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolls_Products_IdP",
                table: "Enrolls",
                column: "IdP",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
