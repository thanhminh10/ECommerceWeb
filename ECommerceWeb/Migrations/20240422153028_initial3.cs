using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ECommerceWeb.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });
            migrationBuilder.AddColumn<int>(
                    name: "BrandId",
                    table: "Product",
                    nullable: true);


            migrationBuilder.AddForeignKey(
                   name: "FK_Product_Brand_BrandId",
                   table: "Product",
                   column: "BrandId",
                   principalTable: "Brand",
                   principalColumn: "Id",
                  onDelete: ReferentialAction.Cascade);

            migrationBuilder.AlterColumn<int>(
                    name: "CategoryId",
                    table: "Product",
                    nullable: true, // Change this from false to true
                    oldClrType: typeof(int),
                    oldType: "OldDataType",
                    oldNullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Product");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Brand");
        }
    }
}
