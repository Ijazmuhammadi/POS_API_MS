using Microsoft.EntityFrameworkCore.Migrations;

namespace POS_API.Migrations
{
    public partial class order_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Order_Name",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order_Name",
                table: "orders");
        }
    }
}
