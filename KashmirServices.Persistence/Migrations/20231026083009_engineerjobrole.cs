using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KashmirServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class engineerjobrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EngineerJobRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EngineerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineerJobRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngineerJobRoles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EngineerJobRoles_Users_EngineerId",
                        column: x => x.EngineerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("0216a0dd-561f-4ba7-8c47-43fbcb0dfbd7"), new DateTimeOffset(new DateTime(2023, 10, 26, 14, 0, 9, 382, DateTimeKind.Unspecified).AddTicks(1991), new TimeSpan(0, 5, 30, 0, 0)), new Guid("1a46d89d-e7b3-457d-8cf8-fc5097c83834"), new DateTimeOffset(new DateTime(2023, 10, 26, 14, 0, 9, 382, DateTimeKind.Unspecified).AddTicks(2020), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_EngineerJobRoles_CategoryId",
                table: "EngineerJobRoles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EngineerJobRoles_EngineerId",
                table: "EngineerJobRoles",
                column: "EngineerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EngineerJobRoles");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
                columns: new[] { "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("aa64c2c5-7c49-4c08-89b7-1ecd2b3fe70e"), new DateTimeOffset(new DateTime(2023, 10, 26, 12, 4, 48, 370, DateTimeKind.Unspecified).AddTicks(3465), new TimeSpan(0, 5, 30, 0, 0)), new Guid("5c701fe9-7a35-448c-8994-d442e119696d"), new DateTimeOffset(new DateTime(2023, 10, 26, 12, 4, 48, 370, DateTimeKind.Unspecified).AddTicks(3498), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
