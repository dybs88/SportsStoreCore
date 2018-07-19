using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SportsStore.Migrations
{
    public partial class CustomerIdOrderFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Addresses_AddressId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                schema: "Sales",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Addresses_AddressId",
                schema: "Sales",
                table: "SalesOrders",
                column: "AddressId",
                principalSchema: "Customer",
                principalTable: "Addresses",
                principalColumn: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Customers_CustomerId",
                schema: "Sales",
                table: "SalesOrders",
                column: "CustomerId",
                principalSchema: "Customer",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Addresses_AddressId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                schema: "Sales",
                table: "SalesOrders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
