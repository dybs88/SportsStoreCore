using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SportsStore.Migrations
{
    public partial class CustomerAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_SalesOrders_OrderId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Products_ProductID",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Addresses_AddressId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_AddressId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "Customer",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "CartItem",
                newName: "Items",
                newSchema: "Sales");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_ProductID",
                schema: "Sales",
                table: "Items",
                newName: "IX_Items_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_OrderId",
                schema: "Sales",
                table: "Items",
                newName: "IX_Items_OrderId");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                schema: "Sales",
                table: "SalesOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                schema: "Sales",
                table: "Items",
                column: "CartItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_CustomerId",
                schema: "Sales",
                table: "SalesOrders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_SalesOrders_OrderId",
                schema: "Sales",
                table: "Items",
                column: "OrderId",
                principalSchema: "Sales",
                principalTable: "SalesOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Products_ProductID",
                schema: "Sales",
                table: "Items",
                column: "ProductID",
                principalSchema: "Store",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Customers_CustomerId",
                schema: "Sales",
                table: "SalesOrders",
                column: "CustomerId",
                principalSchema: "Customer",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_SalesOrders_OrderId",
                schema: "Sales",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Products_ProductID",
                schema: "Sales",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Customers_CustomerId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_CustomerId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                schema: "Sales",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.RenameTable(
                name: "Items",
                schema: "Sales",
                newName: "CartItem");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ProductID",
                table: "CartItem",
                newName: "IX_CartItem_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_Items_OrderId",
                table: "CartItem",
                newName: "IX_CartItem_OrderId");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                schema: "Sales",
                table: "SalesOrders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                schema: "Customer",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                column: "CartItemId");

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "Customer",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApartmentNumber = table.Column<string>(nullable: true),
                    BuildingNumber = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    Region = table.Column<string>(nullable: false),
                    Street = table.Column<string>(nullable: false),
                    ZipCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_AddressId",
                schema: "Sales",
                table: "SalesOrders",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_SalesOrders_OrderId",
                table: "CartItem",
                column: "OrderId",
                principalSchema: "Sales",
                principalTable: "SalesOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Products_ProductID",
                table: "CartItem",
                column: "ProductID",
                principalSchema: "Store",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Addresses_AddressId",
                schema: "Sales",
                table: "SalesOrders",
                column: "AddressId",
                principalSchema: "Customer",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
