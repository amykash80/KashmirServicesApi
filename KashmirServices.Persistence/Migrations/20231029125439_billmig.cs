using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KashmirServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class billmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitNo",
                table: "Visits");

            migrationBuilder.AddColumn<double>(
                name: "TotalDistance",
                table: "Visits",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SerialNo",
                table: "Parts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("f5ea5b45-4a16-4734-a03e-f226ed7799bc"), new DateTimeOffset(new DateTime(2023, 10, 29, 18, 24, 39, 256, DateTimeKind.Unspecified).AddTicks(7648), new TimeSpan(0, 5, 30, 0, 0)), new Guid("0ae573f0-e772-47b6-a414-41786de9f2e8"), new DateTimeOffset(new DateTime(2023, 10, 29, 18, 24, 39, 256, DateTimeKind.Unspecified).AddTicks(7687), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDistance",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "SerialNo",
                table: "Parts");

            migrationBuilder.AddColumn<int>(
                name: "VisitNo",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("a4ff51f7-6608-4e65-a1d8-742bc4702713"), new DateTimeOffset(new DateTime(2023, 10, 29, 13, 17, 41, 652, DateTimeKind.Unspecified).AddTicks(1914), new TimeSpan(0, 5, 30, 0, 0)), new Guid("1a00f8ff-c7ce-455c-ba36-0792978a0672"), new DateTimeOffset(new DateTime(2023, 10, 29, 13, 17, 41, 652, DateTimeKind.Unspecified).AddTicks(1944), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
