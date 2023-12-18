using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KashmirServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class assignedEngineerUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "AssignedEngineers");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ExpectedDate",
                table: "AssignedEngineers",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("a896ff80-05d3-4744-9e80-f8a0b56475b6"), new DateTimeOffset(new DateTime(2023, 10, 28, 23, 38, 24, 879, DateTimeKind.Unspecified).AddTicks(5073), new TimeSpan(0, 5, 30, 0, 0)), new Guid("cdeb950f-84df-4412-a4a5-4815f733df89"), new DateTimeOffset(new DateTime(2023, 10, 28, 23, 38, 24, 879, DateTimeKind.Unspecified).AddTicks(5128), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ExpectedDate",
                table: "AssignedEngineers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "AssignedEngineers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("e2b47614-a8cc-4eec-ae9b-f874d97a8405"), new DateTimeOffset(new DateTime(2023, 10, 28, 14, 5, 6, 772, DateTimeKind.Unspecified).AddTicks(9961), new TimeSpan(0, 5, 30, 0, 0)), new Guid("a5593025-67f2-4b1e-88ae-a7beb68e9e36"), new DateTimeOffset(new DateTime(2023, 10, 28, 14, 5, 6, 772, DateTimeKind.Unspecified).AddTicks(9994), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
