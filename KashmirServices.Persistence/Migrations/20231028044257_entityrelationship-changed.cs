using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KashmirServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class entityrelationshipchanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CallBookings_Brands_BrandId",
                table: "CallBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Categories_CategoryId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTypes_Categories_CategoryId",
                table: "ServiceTypes");

            migrationBuilder.DropIndex(
                name: "IX_Parts_CategoryId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_CallBookings_BrandId",
                table: "CallBookings");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "CallBookings");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ServiceTypes",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceTypes_CategoryId",
                table: "ServiceTypes",
                newName: "IX_ServiceTypes_BrandId");

            migrationBuilder.AddColumn<int>(
                name: "Reminder",
                table: "CallBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("60b1fc0d-2dba-44f9-bf7b-1e177b0a70f2"), new DateTimeOffset(new DateTime(2023, 10, 28, 10, 12, 57, 491, DateTimeKind.Unspecified).AddTicks(8851), new TimeSpan(0, 5, 30, 0, 0)), new Guid("8423d3d0-f1a9-4996-b262-7a5a1e6243eb"), new DateTimeOffset(new DateTime(2023, 10, 28, 10, 12, 57, 491, DateTimeKind.Unspecified).AddTicks(8890), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTypes_Brands_BrandId",
                table: "ServiceTypes",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTypes_Brands_BrandId",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "Reminder",
                table: "CallBookings");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "ServiceTypes",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceTypes_BrandId",
                table: "ServiceTypes",
                newName: "IX_ServiceTypes_CategoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Parts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BrandId",
                table: "CallBookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("e4d76d3c-5f68-4777-b5e6-1f8475537052"), new DateTimeOffset(new DateTime(2023, 10, 27, 23, 37, 32, 6, DateTimeKind.Unspecified).AddTicks(1583), new TimeSpan(0, 5, 30, 0, 0)), new Guid("b61d0b6d-0430-4217-a0cf-48d05c46bd72"), new DateTimeOffset(new DateTime(2023, 10, 27, 23, 37, 32, 6, DateTimeKind.Unspecified).AddTicks(1613), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_CategoryId",
                table: "Parts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CallBookings_BrandId",
                table: "CallBookings",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_CallBookings_Brands_BrandId",
                table: "CallBookings",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Categories_CategoryId",
                table: "Parts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTypes_Categories_CategoryId",
                table: "ServiceTypes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
