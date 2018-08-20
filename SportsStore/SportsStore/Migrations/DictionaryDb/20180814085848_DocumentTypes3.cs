using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations.DictionaryDb
{
    public partial class DocumentTypes3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentType",
                table: "DocumentType");

            migrationBuilder.EnsureSchema(
                name: "Dictionary");

            migrationBuilder.RenameTable(
                name: "DocumentType",
                newName: "DocumentTypes",
                newSchema: "Dictionary");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentTypes",
                schema: "Dictionary",
                table: "DocumentTypes",
                column: "DocumentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentTypes",
                schema: "Dictionary",
                table: "DocumentTypes");

            migrationBuilder.RenameTable(
                name: "DocumentTypes",
                schema: "Dictionary",
                newName: "DocumentType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentType",
                table: "DocumentType",
                column: "DocumentTypeId");
        }
    }
}
