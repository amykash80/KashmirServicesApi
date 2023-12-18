using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KashmirServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class brandRelationcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Brands",
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
                name: "IX_Brands_CategoryId",
                table: "Brands",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Categories_CategoryId",
                table: "Brands",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Categories_CategoryId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_CategoryId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Brands");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("013fd8c2-95b9-46f0-8cf6-e10f3065a758"), new DateTimeOffset(new DateTime(2023, 10, 27, 22, 51, 58, 250, DateTimeKind.Unspecified).AddTicks(5768), new TimeSpan(0, 5, 30, 0, 0)), new Guid("f0ebabc6-eff7-4ba4-a6e4-424478ddc927"), new DateTimeOffset(new DateTime(2023, 10, 27, 22, 51, 58, 250, DateTimeKind.Unspecified).AddTicks(5806), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
