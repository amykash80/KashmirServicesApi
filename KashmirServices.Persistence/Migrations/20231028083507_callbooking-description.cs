using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KashmirServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class callbookingdescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FaultDetails",
                table: "CallBookings",
                newName: "Description");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("e2b47614-a8cc-4eec-ae9b-f874d97a8405"), new DateTimeOffset(new DateTime(2023, 10, 28, 14, 5, 6, 772, DateTimeKind.Unspecified).AddTicks(9961), new TimeSpan(0, 5, 30, 0, 0)), new Guid("a5593025-67f2-4b1e-88ae-a7beb68e9e36"), new DateTimeOffset(new DateTime(2023, 10, 28, 14, 5, 6, 772, DateTimeKind.Unspecified).AddTicks(9994), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "CallBookings",
                newName: "FaultDetails");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("0a6316c4-26c9-4361-903c-992fa3d61e2d"), new DateTimeOffset(new DateTime(2023, 10, 28, 12, 19, 50, 662, DateTimeKind.Unspecified).AddTicks(4212), new TimeSpan(0, 5, 30, 0, 0)), new Guid("30f10ef5-0cb7-4c54-81f8-7764a6f93472"), new DateTimeOffset(new DateTime(2023, 10, 28, 12, 19, 50, 662, DateTimeKind.Unspecified).AddTicks(4246), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
