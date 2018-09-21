using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations.DictionaryDb
{
    public partial class VatRate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VatRate",
                schema: "Dictionary",
                table: "VatRate");

            migrationBuilder.RenameTable(
                name: "VatRate",
                schema: "Dictionary",
                newName: "VatRates",
                newSchema: "Dictionary");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VatRates",
                schema: "Dictionary",
                table: "VatRates",
                column: "VatRateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VatRates",
                schema: "Dictionary",
                table: "VatRates");

            migrationBuilder.RenameTable(
                name: "VatRates",
                schema: "Dictionary",
                newName: "VatRate",
                newSchema: "Dictionary");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VatRate",
                schema: "Dictionary",
                table: "VatRate",
                column: "VatRateId");
        }
    }
}
