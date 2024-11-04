using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBookImageUrlRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Books",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: false,
                defaultValue: "",
                comment: "Book cover image",
                oldClrType: typeof(string),
                oldType: "nvarchar(2083)",
                oldMaxLength: 2083,
                oldNullable: true,
                oldComment: "Book cover image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Books",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: true,
                comment: "Book cover image",
                oldClrType: typeof(string),
                oldType: "nvarchar(2083)",
                oldMaxLength: 2083,
                oldComment: "Book cover image");           
        }
    }
}
