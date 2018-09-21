using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations.DictionaryDb
{
    public partial class VatRate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                schema: "Dictionary",
                table: "VatRates",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "Dictionary",
                table: "VatRates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_VatRates_Symbol",
                schema: "Dictionary",
                table: "VatRates",
                column: "Symbol",
                unique: true,
                filter: "[Symbol] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VatRates_Symbol",
                schema: "Dictionary",
                table: "VatRates");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "Dictionary",
                table: "VatRates");

            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                schema: "Dictionary",
                table: "VatRates",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
