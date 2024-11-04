using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("12b8e696-6761-43f7-8bd8-2af7a82c8afb"), false, "Poetry" },
                    { new Guid("2e2d89db-082f-403a-98a8-fee8a9e5908c"), false, "Plays" },
                    { new Guid("2fa5129c-d729-45af-868c-996ad36b37da"), false, "Magical Realism" },
                    { new Guid("3413e755-cfc3-4fb7-9bc5-9e3314159cd9"), false, "Dystopia" },
                    { new Guid("3499ce22-d7b4-4ad1-9a4c-aadb28e1dc72"), false, "Mythology" },
                    { new Guid("61f871f9-1e36-4f20-be48-4749c171da7e"), false, "Historical Fiction" },
                    { new Guid("70ce3360-b547-407e-baf4-798fb9dba878"), false, "Fiction" },
                    { new Guid("9d5f3715-6ad0-4b07-80b7-7b515db5a223"), false, "Childrens" },
                    { new Guid("a26137f3-aec0-475e-8587-83477cd66e43"), false, "Short Stories" },
                    { new Guid("a6be0772-da8b-4712-9169-5863964c1996"), false, "Short Novel" },
                    { new Guid("aaeaf366-911b-4b96-9977-05076127d8dd"), false, "Fantasy" },
                    { new Guid("b0823402-4dda-4b81-b587-915749f2605b"), false, "Philosophy" },
                    { new Guid("d634aa2a-17c8-46d8-8fb4-9611da7e59fc"), false, "Religion" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("12b8e696-6761-43f7-8bd8-2af7a82c8afb"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2e2d89db-082f-403a-98a8-fee8a9e5908c"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2fa5129c-d729-45af-868c-996ad36b37da"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3413e755-cfc3-4fb7-9bc5-9e3314159cd9"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3499ce22-d7b4-4ad1-9a4c-aadb28e1dc72"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("61f871f9-1e36-4f20-be48-4749c171da7e"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("70ce3360-b547-407e-baf4-798fb9dba878"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("9d5f3715-6ad0-4b07-80b7-7b515db5a223"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a26137f3-aec0-475e-8587-83477cd66e43"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a6be0772-da8b-4712-9169-5863964c1996"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("aaeaf366-911b-4b96-9977-05076127d8dd"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b0823402-4dda-4b81-b587-915749f2605b"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("d634aa2a-17c8-46d8-8fb4-9611da7e59fc"));            
        }
    }
}
