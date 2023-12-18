using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KashmirServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class relationshipmodfied : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_ServiceTypes_ServiceTypeId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ServiceTypeId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("0a6316c4-26c9-4361-903c-992fa3d61e2d"), new DateTimeOffset(new DateTime(2023, 10, 28, 12, 19, 50, 662, DateTimeKind.Unspecified).AddTicks(4212), new TimeSpan(0, 5, 30, 0, 0)), new Guid("30f10ef5-0cb7-4c54-81f8-7764a6f93472"), new DateTimeOffset(new DateTime(2023, 10, 28, 12, 19, 50, 662, DateTimeKind.Unspecified).AddTicks(4246), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ServiceTypeId",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("60b1fc0d-2dba-44f9-bf7b-1e177b0a70f2"), new DateTimeOffset(new DateTime(2023, 10, 28, 10, 12, 57, 491, DateTimeKind.Unspecified).AddTicks(8851), new TimeSpan(0, 5, 30, 0, 0)), new Guid("8423d3d0-f1a9-4996-b262-7a5a1e6243eb"), new DateTimeOffset(new DateTime(2023, 10, 28, 10, 12, 57, 491, DateTimeKind.Unspecified).AddTicks(8890), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ServiceTypeId",
                table: "Invoices",
                column: "ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_ServiceTypes_ServiceTypeId",
                table: "Invoices",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
