using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SportsStore.Migrations
{
    public partial class OrderNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_SalesOrders_OrderId",
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

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                schema: "Sales",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                schema: "Sales",
                table: "SalesOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                schema: "Sales",
                table: "SalesOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                schema: "Sales",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "Customer",
                table: "Customers",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                schema: "Customer",
                table: "Addresses",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                schema: "Customer",
                table: "Addresses",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                schema: "Customer",
                table: "Addresses",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "City",
                schema: "Customer",
                table: "Addresses",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "BuildingNumber",
                schema: "Customer",
                table: "Addresses",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ApartmentNumber",
                schema: "Customer",
                table: "Addresses",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_AddressId",
                schema: "Sales",
                table: "SalesOrders",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_SalesOrders_OrderId",
                schema: "Sales",
                table: "Items",
                column: "OrderId",
                principalSchema: "Sales",
                principalTable: "SalesOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_SalesOrders_OrderId",
                schema: "Sales",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Addresses_AddressId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_AddressId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                schema: "Sales",
                table: "SalesOrders");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                schema: "Sales",
                table: "SalesOrders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Sales",
                table: "SalesOrders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                schema: "Sales",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "Customer",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                schema: "Customer",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75);

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                schema: "Customer",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                schema: "Customer",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                schema: "Customer",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75);

            migrationBuilder.AlterColumn<string>(
                name: "BuildingNumber",
                schema: "Customer",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<string>(
                name: "ApartmentNumber",
                schema: "Customer",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6,
                oldNullable: true);

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
                name: "FK_SalesOrders_Customers_CustomerId",
                schema: "Sales",
                table: "SalesOrders",
                column: "CustomerId",
                principalSchema: "Customer",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
