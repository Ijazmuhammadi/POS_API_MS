using Microsoft.EntityFrameworkCore.Migrations;

namespace POS_API.Migrations
{
    public partial class producttypechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_products_ProductID",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_ProductID",
                table: "orders");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "orders");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ProductID",
                table: "orders",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_products_ProductID",
                table: "orders",
                column: "ProductID",
                principalTable: "products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
