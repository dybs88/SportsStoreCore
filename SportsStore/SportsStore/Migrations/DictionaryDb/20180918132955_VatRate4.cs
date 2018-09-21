using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations.DictionaryDb
{
    public partial class VatRate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                schema: "Dictionary",
                table: "VatRates",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                schema: "Dictionary",
                table: "VatRates",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
