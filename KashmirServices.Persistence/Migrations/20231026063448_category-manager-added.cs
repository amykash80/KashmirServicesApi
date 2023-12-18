using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KashmirServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class categorymanageradded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSolutions_Users_ManagerId",
                table: "ProductSolutions");

            migrationBuilder.DropIndex(
                name: "IX_ProductSolutions_ManagerId",
                table: "ProductSolutions");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "ProductSolutions");

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("aa64c2c5-7c49-4c08-89b7-1ecd2b3fe70e"), new DateTimeOffset(new DateTime(2023, 10, 26, 12, 4, 48, 370, DateTimeKind.Unspecified).AddTicks(3465), new TimeSpan(0, 5, 30, 0, 0)), new Guid("5c701fe9-7a35-448c-8994-d442e119696d"), new DateTimeOffset(new DateTime(2023, 10, 26, 12, 4, 48, 370, DateTimeKind.Unspecified).AddTicks(3498), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ManagerId",
                table: "Categories",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_ManagerId",
                table: "Categories",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_ManagerId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ManagerId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Categories");

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "ProductSolutions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("63dcd667-23ba-415a-b618-80a31ef23ffb"), new DateTimeOffset(new DateTime(2023, 10, 26, 11, 2, 10, 512, DateTimeKind.Unspecified).AddTicks(7203), new TimeSpan(0, 5, 30, 0, 0)), new Guid("01e95bd7-4a2c-437e-88c7-5333a01a6c5b"), new DateTimeOffset(new DateTime(2023, 10, 26, 11, 2, 10, 512, DateTimeKind.Unspecified).AddTicks(7235), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSolutions_ManagerId",
                table: "ProductSolutions",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSolutions_Users_ManagerId",
                table: "ProductSolutions",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
