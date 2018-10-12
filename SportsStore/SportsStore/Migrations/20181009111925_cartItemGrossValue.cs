using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations
{
    public partial class cartItemGrossValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                schema: "Sales",
                table: "Items",
                newName: "NetValue");

            migrationBuilder.AddColumn<decimal>(
                name: "GrossValue",
                schema: "Sales",
                table: "Items",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossValue",
                schema: "Sales",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "NetValue",
                schema: "Sales",
                table: "Items",
                newName: "Value");
        }
    }
}
