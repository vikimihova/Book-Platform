using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreAuthors_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "FullName", "IsDeleted", "IsSubmittedByUser", "LastName" },
                values: new object[] { new Guid("0d418357-bd20-46c2-869a-d9a7ee6193e9"), "Rudyard", "Rudyard Kipling", false, false, "Kipling" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("0d418357-bd20-46c2-869a-d9a7ee6193e9"));            
        }
    }
}
