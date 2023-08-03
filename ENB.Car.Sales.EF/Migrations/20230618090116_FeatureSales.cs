using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENB.Car.Sales.EF.Migrations
{
    /// <inheritdoc />
    public partial class FeatureSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8269890c-1ca3-4570-a4e2-396aa72815b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b982f4d0-b8de-4c58-94f0-2880032ab5b0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c613d2f1-dd9e-46c6-9696-1bd97ce4ce7f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dff413d2-51f1-43d0-9a75-1ab6c3cca4c6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1694d51b-96ce-4321-8e25-a2601696f895", null, "Visitor", "VISITOR" },
                    { "2201cc13-6c0b-41bb-aea1-d1ed35b9223b", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1694d51b-96ce-4321-8e25-a2601696f895");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2201cc13-6c0b-41bb-aea1-d1ed35b9223b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a1bb9f2-a099-427c-9f64-e3b8c999857c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f541896a-3a14-485f-a56c-360105cff76c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8269890c-1ca3-4570-a4e2-396aa72815b9", null, "Administrator", "ADMINISTRATOR" },
                    { "dff413d2-51f1-43d0-9a75-1ab6c3cca4c6", null, "Visitor", "VISITOR" }
                });
        }
    }
}
