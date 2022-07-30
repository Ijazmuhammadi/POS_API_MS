using Microsoft.EntityFrameworkCore.Migrations;

namespace POS_API.Migrations
{
    public partial class updateproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryID",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_CategoryID",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "products");

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryID",
                table: "products",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryID",
                table: "products",
                column: "CategoryID",
                principalTable: "categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
