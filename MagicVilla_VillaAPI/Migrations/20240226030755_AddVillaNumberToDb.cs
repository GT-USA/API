using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaNumberToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VillasNumber_API",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillasNumber_API", x => x.VillaNo);
                });

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 25, 22, 7, 53, 870, DateTimeKind.Local).AddTicks(5477));

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 25, 22, 7, 53, 870, DateTimeKind.Local).AddTicks(5515));

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 25, 22, 7, 53, 870, DateTimeKind.Local).AddTicks(5517));

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 25, 22, 7, 53, 870, DateTimeKind.Local).AddTicks(5519));

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 25, 22, 7, 53, 870, DateTimeKind.Local).AddTicks(5521));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillasNumber_API");

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 23, 15, 40, 55, 622, DateTimeKind.Local).AddTicks(479));

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 23, 15, 40, 55, 622, DateTimeKind.Local).AddTicks(522));

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 23, 15, 40, 55, 622, DateTimeKind.Local).AddTicks(524));

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 23, 15, 40, 55, 622, DateTimeKind.Local).AddTicks(526));

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 2, 23, 15, 40, 55, 622, DateTimeKind.Local).AddTicks(528));
        }
    }
}
