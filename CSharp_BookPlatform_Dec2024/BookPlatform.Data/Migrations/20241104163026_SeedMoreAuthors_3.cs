using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreAuthors_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("0d418357-bd20-46c2-869a-d9a7ee6193e9"));

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "FullName", "IsDeleted", "IsSubmittedByUser", "LastName" },
                values: new object[] { new Guid("423470c3-8a18-4a78-84f2-be7e9ec60a5f"), "Rudyard", "Rudyard Kipling", false, false, "Kipling" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("423470c3-8a18-4a78-84f2-be7e9ec60a5f"));

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "FullName", "IsDeleted", "IsSubmittedByUser", "LastName" },
                values: new object[] { new Guid("0d418357-bd20-46c2-869a-d9a7ee6193e9"), "Rudyard", "Rudyard Kipling", false, false, "Kipling" });
        }
    }
}
