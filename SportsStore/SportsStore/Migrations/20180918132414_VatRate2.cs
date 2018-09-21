using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations
{
    public partial class VatRate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "Store",
                table: "Products",
                newName: "NetPrice");

            migrationBuilder.RenameColumn(
                name: "Value",
                schema: "Sales",
                table: "SalesOrders",
                newName: "NetValue");

            migrationBuilder.AddColumn<decimal>(
                name: "GrossPrice",
                schema: "Store",
                table: "Products",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "VatRateId",
                schema: "Store",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "GrossValue",
                schema: "Sales",
                table: "SalesOrders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "VatRateId",
                schema: "Sales",
                table: "SalesOrders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossPrice",
                schema: "Store",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "VatRateId",
                schema: "Store",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GrossValue",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "VatRateId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.RenameColumn(
                name: "NetPrice",
                schema: "Store",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "NetValue",
                schema: "Sales",
                table: "SalesOrders",
                newName: "Value");
        }
    }
}
