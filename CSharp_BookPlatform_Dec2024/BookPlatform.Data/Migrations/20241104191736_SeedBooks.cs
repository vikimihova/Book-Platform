using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "AverageRating", "Description", "GenreId", "ImageUrl", "IsDeleted", "IsSubmittedByUser", "PublicationYear", "Title" },
                values: new object[] { new Guid("50ddee0f-f47b-47af-8f8e-70a9757419aa"), new Guid("69a54e30-11cd-495c-a222-2b0fd492b8a4"), 0.0, "description", new Guid("aaeaf366-911b-4b96-9977-05076127d8dd"), "images/hp-the-philosophers-stone", false, false, 1997, "Harry Potter and the Philosopher's Stone" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("50ddee0f-f47b-47af-8f8e-70a9757419aa"));
        }
    }
}
