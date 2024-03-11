using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaID",
                table: "VillasNumber_API",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 3, 14, 4, 54, 324, DateTimeKind.Local).AddTicks(4282));

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 3, 14, 4, 54, 324, DateTimeKind.Local).AddTicks(4324));

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 3, 14, 4, 54, 324, DateTimeKind.Local).AddTicks(4327));

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 3, 14, 4, 54, 324, DateTimeKind.Local).AddTicks(4329));

            migrationBuilder.UpdateData(
                table: "Villas_API",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 3, 14, 4, 54, 324, DateTimeKind.Local).AddTicks(4331));

            migrationBuilder.CreateIndex(
                name: "IX_VillasNumber_API_VillaID",
                table: "VillasNumber_API",
                column: "VillaID");

            migrationBuilder.AddForeignKey(
                name: "FK_VillasNumber_API_Villas_API_VillaID",
                table: "VillasNumber_API",
                column: "VillaID",
                principalTable: "Villas_API",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillasNumber_API_Villas_API_VillaID",
                table: "VillasNumber_API");

            migrationBuilder.DropIndex(
                name: "IX_VillasNumber_API_VillaID",
                table: "VillasNumber_API");

            migrationBuilder.DropColumn(
                name: "VillaID",
                table: "VillasNumber_API");

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
    }
}
