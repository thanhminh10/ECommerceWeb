using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWeb.Migrations
{
    public partial class addproductcount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCount",
                table: "Category",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCount",
                table: "Category");
        }
    }
}
