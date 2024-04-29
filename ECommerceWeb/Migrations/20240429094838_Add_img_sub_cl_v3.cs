using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWeb.Migrations
{
    /// <inheritdoc />
    public partial class Add_img_sub_cl_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL_02",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageURL_03",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL_02",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImageURL_03",
                table: "Product");
        }
    }
}
