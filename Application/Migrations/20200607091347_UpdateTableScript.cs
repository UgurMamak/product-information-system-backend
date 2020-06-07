using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class UpdateTableScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductTypeId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ProductTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                unique: true,
                filter: "[ProductTypeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Product");
        }
    }
}
