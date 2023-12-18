using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KashmirServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class callbookingjobnoidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("a4ff51f7-6608-4e65-a1d8-742bc4702713"), new DateTimeOffset(new DateTime(2023, 10, 29, 13, 17, 41, 652, DateTimeKind.Unspecified).AddTicks(1914), new TimeSpan(0, 5, 30, 0, 0)), new Guid("1a00f8ff-c7ce-455c-ba36-0792978a0672"), new DateTimeOffset(new DateTime(2023, 10, 29, 13, 17, 41, 652, DateTimeKind.Unspecified).AddTicks(1944), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("a896ff80-05d3-4744-9e80-f8a0b56475b6"), new DateTimeOffset(new DateTime(2023, 10, 28, 23, 38, 24, 879, DateTimeKind.Unspecified).AddTicks(5073), new TimeSpan(0, 5, 30, 0, 0)), new Guid("cdeb950f-84df-4412-a4a5-4815f733df89"), new DateTimeOffset(new DateTime(2023, 10, 28, 23, 38, 24, 879, DateTimeKind.Unspecified).AddTicks(5128), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
