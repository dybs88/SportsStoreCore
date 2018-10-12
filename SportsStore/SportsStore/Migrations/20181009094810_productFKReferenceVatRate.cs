using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations
{
    public partial class productFKReferenceVatRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Dictionary");

            //migrationBuilder.CreateTable(
            //    name: "VatRates",
            //    schema: "Dictionary",
            //    columns: table => new
            //    {
            //        VatRateId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Symbol = table.Column<string>(nullable: false),
            //        Value = table.Column<decimal>(nullable: false),
            //        IsDefault = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VatRates", x => x.VatRateId);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_Products_VatRateId",
                schema: "Store",
                table: "Products",
                column: "VatRateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_VatRates_VatRateId",
                schema: "Store",
                table: "Products",
                column: "VatRateId",
                principalSchema: "Dictionary",
                principalTable: "VatRates",
                principalColumn: "VatRateId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_VatRates_VatRateId",
                schema: "Store",
                table: "Products");

            migrationBuilder.DropTable(
                name: "VatRates",
                schema: "Dictionary");

            migrationBuilder.DropIndex(
                name: "IX_Products_VatRateId",
                schema: "Store",
                table: "Products");
        }
    }
}
