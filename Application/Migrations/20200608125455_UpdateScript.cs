using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class UpdateScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeId1",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId1",
                table: "Product",
                column: "ProductTypeId1",
                unique: true,
                filter: "[ProductTypeId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_ProductTypeId1",
                table: "Product",
                column: "ProductTypeId1",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_ProductTypeId1",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductTypeId1",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductTypeId1",
                table: "Product");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                unique: true,
                filter: "[ProductTypeId] IS NOT NULL");
        }
    }
}
