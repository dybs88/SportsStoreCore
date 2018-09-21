using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations.DictionaryDb
{
    public partial class VatRate6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VatRates_Symbol",
                schema: "Dictionary",
                table: "VatRates");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                schema: "Dictionary",
                table: "VatRates",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                schema: "Dictionary",
                table: "VatRates",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VatRates_Symbol",
                schema: "Dictionary",
                table: "VatRates",
                column: "Symbol",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VatRates_Symbol",
                schema: "Dictionary",
                table: "VatRates");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                schema: "Dictionary",
                table: "VatRates",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                schema: "Dictionary",
                table: "VatRates",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_VatRates_Symbol",
                schema: "Dictionary",
                table: "VatRates",
                column: "Symbol",
                unique: true,
                filter: "[Symbol] IS NOT NULL");
        }
    }
}
