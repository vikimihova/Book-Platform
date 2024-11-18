using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class NonNullablePublicationYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PublicationYear",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Official known first publication year of the book",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Official known first publication year of the book");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PublicationYear",
                table: "Books",
                type: "int",
                nullable: true,
                comment: "Official known first publication year of the book",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Official known first publication year of the book");
        }
    }
}
