using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreAuthors_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "FullName", "IsDeleted", "IsSubmittedByUser", "LastName" },
                values: new object[,]
                {
                    { new Guid("d8a5ac42-01b0-49ab-a1b1-447b99d1768b"), "Richard", "Richard Adams", false, false, "Adams" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("d8a5ac42-01b0-49ab-a1b1-447b99d1768b"));
        }
    }
}
