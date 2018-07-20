using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SportsStore.Migrations
{
    public partial class ItemsOrdersValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentTypeId",
                schema: "Sales",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                schema: "Sales",
                table: "SalesOrders",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                schema: "Sales",
                table: "Items",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_CustomerId",
                schema: "Sales",
                table: "SalesOrders",
                column: "CustomerId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_SalesOrders_Customers_CustomerId",
            //    schema: "Sales",
            //    table: "SalesOrders",
            //    column: "CustomerId",
            //    principalSchema: "Customer",
            //    principalTable: "Customers",
            //    principalColumn: "CustomerId",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Customers_CustomerId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_CustomerId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "DocumentTypeId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "Sales",
                table: "Items");
        }
    }
}
